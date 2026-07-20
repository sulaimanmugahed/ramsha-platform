using Asp.Versioning;
using Ramsha.Application.DTOs.Account.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ramsha.Application.DTOs.Account.Requests;
using Ramsha.Application.Dtos.Account.Requests;
using Ramsha.Contracts;
using Ramsha.Application.Services.Identity;
using Ramsha.Api.Infrastructure.Services;
using Ramsha.Shared;
using Ramsha.Api.Constants;
using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.Features.Baskets.Queries.GetBasket;
using Ramsha.Contracts.Baskets.Commands;
using Ramsha.Api.Extensions;
using Ramsha.Application.Services.Identity.UserInterfaces;


namespace Ramsha.Api.Controllers.v1;


/// <summary>
/// Manages account-related operations.
/// </summary>
[ApiVersion("1.0")]
public class AccountController(IUserService userService, IAuthenticatedUserService authenticatedUserService, IAccountServices accountServices, ICookieService cookieService) : BaseApiController
{

	/// <summary>
	/// Authenticates the user
	/// </summary>
	/// <remarks>
	/// This endpoint authenticates the user and provides an access token for subsequent requests.
	/// </remarks>
	[HttpPost("login")]
	public async Task<ActionResult<AuthenticatedUserDto?>> Login([FromBody] LoginRequest request)
	{
		return RamshaResult(await TransactionalUnitOfWork<IRamshaResult>(async () =>
		{
			var result = await accountServices.Authenticate(
						new() { UserName = request.Username, Password = request.Password });

			if (!result.Succeeded)
			{
				return result.Error;
			}

			if (!string.IsNullOrEmpty(result.Value?.RefreshToken))
			{
				cookieService.SetCookieValue(ApplicationCookies.RefreshToken,
					result.Value.RefreshToken,
					new()
					{
						HttpOnly = true,
						Expires = result.Value.RefreshTokenExpiration.ToLocalTime(),
						SameSite = SameSiteMode.Lax,
						Secure = true,
					});
			}


			var dto = result.Value!.Role switch
			{
				Roles.Customer => result.Value.AsAuthCustomerDto(await GetAndHandleCustomerBasket(result.Value.Username)),
				_ => result.Value.AsAuthUserDto()
			};

			return RamshaResults.Success(dto);
		}));
	}

	private async Task<BasketDto?> GetAndHandleCustomerBasket(string username)
	{
		var authCustomerBasket = (await Mediator.Send(new GetBasketQuery { Buyer = username })).Value;

		var anonCustomer = cookieService.GetCookieValue(ApplicationCookies.Buyer);
		var anonCustomerBasket = (await Mediator.Send(new GetBasketQuery { Buyer = anonCustomer })).Value;

		if (anonCustomerBasket is not null)
		{
			if (authCustomerBasket is not null)
			{
				await Mediator.Send(new DeleteBasketCommand(authCustomerBasket.Id));
			}

			await Mediator.Send(new ChangeBasketBuyerCommand(anonCustomerBasket.Id, username));
			cookieService.RemoveCookie(ApplicationCookies.Buyer);
		}

		return anonCustomerBasket ?? authCustomerBasket;
	}

	/// <summary>
	/// Refresh the token
	/// </summary>
	/// <remarks>
	/// This endpoint issues a new access token using a valid refresh token to maintain session continuity.
	/// </remarks>

	[HttpPost("refresh")]
	public async Task<ActionResult<AuthenticatedUserDto?>> Refresh([FromBody] RefreshRequest request)
	{
		var refreshToken = cookieService.GetCookieValue(ApplicationCookies.RefreshToken);
		if (refreshToken is null)
			return RamshaResult(RamshaResults.Unauthenticated(message: "no refresh token found in the cookie"));

		var result = await accountServices.Refresh(refreshToken, request.AccessToken);
		if (!result.Succeeded)
			return RamshaResult(result.Error);

		if (!string.IsNullOrEmpty(result.Value?.RefreshToken))
		{
			cookieService.SetCookieValue(ApplicationCookies.RefreshToken,
				result.Value.RefreshToken,
				new()
				{
					HttpOnly = true,
					Expires = result.Value.RefreshTokenExpiration.ToLocalTime(),
					SameSite = SameSiteMode.Lax,
					Secure = true,
				});
		}

		var dto = result.Value.Role switch
		{
			Roles.Customer => result.Value.AsAuthCustomerDto(await GetCustomerBasket(result.Value.Username)),
			_ => result.Value.AsAuthUserDto()
		};

		return RamshaResult(RamshaResults.Success(dto));
	}

	private async Task<BasketDto?> GetCustomerBasket(string username)
	{
		return (await Mediator.Send(new GetBasketQuery { Buyer = username })).Value;
	}



	/// <summary>
	/// Revoke the token
	/// </summary>
	/// <remarks>
	/// This endpoint invalidates the refresh token, terminating the session and preventing further token generation.
	/// </remarks>
	[HttpDelete("revoke")]
	public async Task<IActionResult> Revoke()
	{
		var token = cookieService.GetCookieValue(ApplicationCookies.RefreshToken);
		if (token is null)
			return RamshaResult(RamshaResults.Unauthenticated(message: "no refresh token in cookie"));
		var result = await accountServices.Revoke(token);

		if (result.Succeeded)
			cookieService.RemoveCookie(ApplicationCookies.RefreshToken);

		return RamshaResult(result);

	}


	/// <summary>
	/// Logout the user
	/// </summary>
	/// <remarks>
	/// This endpoint ends the user session by invalidating both the access and refresh tokens.
	/// </remarks>
	[HttpPost("logout")]
	public async Task<IActionResult> LogoutUser()
	{

		var token = cookieService.GetCookieValue(ApplicationCookies.RefreshToken);
		if (token is null)
			return Unauthorized();

		var result = await accountServices.Revoke(token);

		if (result.Succeeded)
			cookieService.RemoveCookie(ApplicationCookies.RefreshToken);

		await accountServices.LogoutCurrentUser();

		return Ok();

	}


	/// <summary>
	/// Update the address
	/// </summary>
	/// <remarks>
	/// This endpoint create or update the user address.
	/// </remarks>
	[HttpPut("address")]
	public async Task<IActionResult> UpdateAddress(UpdateAddressRequest request)
	{
		await userService.UpdateUserAddress(authenticatedUserService.UserName, request);
		return Ok();
	}


	/// <summary>
	/// Send Confirm Email
	/// </summary>
	/// <remarks>
	/// This endpoint send confirm email link to email provided.
	/// </remarks>
	[HttpPost("send-confirm-email")]
	public async Task<IActionResult> SendConfirmEmail([FromQuery] string email)
	{
		var result = await accountServices.SendConfirmEmail(email);
		if (!result)
			return BadRequest("some thing went wrong,we couldn't send confirm email");

		return Ok();
	}

	/// <summary>
	/// Verify Email
	/// </summary>
	/// <remarks>
	/// This endpoint verify and confirm the email by providing the confirm email token
	/// </remarks>
	[HttpGet("verify-email")]
	public async Task<ActionResult<string>> VerifyEmail(string email, string token)
	{
		var result = await accountServices.VerifyEmail(email, token);
		if (!result)
			return BadRequest("some thing went wrong,we couldn't send confirm email");

		return email;
	}

	/// <summary>
	/// Change Password
	/// </summary>
	/// <remarks>
	/// This endpoint allows changing the password by providing the old password.
	/// </remarks>
	[Authorize]
	[HttpPost("change-password")]
	public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
	{
		var result = await accountServices.ChangePassword(User.Identity.Name, request);
		if (!result)
		{
			return BadRequest("some thing went wrong,we couldn't Change your Password");
		}
		return Ok();
	}

	/// <summary>
	/// Change Password
	/// </summary>
	/// <remarks>
	/// This endpoint allows changing the password by providing the reset password token.
	/// </remarks>
	[Authorize]
	[HttpPost("reset-password")]
	public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
	{
		var result = await accountServices.ResetPassword(User.Identity.Name, request);
		if (!result)
		{
			return BadRequest("some thing went wrong,we couldn't Change your Password");
		}
		return Ok();
	}

	/// <summary>
	/// Send Reset Password Email
	/// </summary>
	/// <remarks>
	/// This endpoint sends a password reset link to the email provided.
	/// </remarks>
	[HttpPost("send-reset-password-email")]
	public async Task<IActionResult> SendResetPasswordEmail()
	{
		var result = await accountServices.SendResetPasswordEmail(authenticatedUserService.UserName);
		if (!result)
			return BadRequest("some thing went wrong,we couldn't send Reset Password Email");
		return Ok();
	}


}
