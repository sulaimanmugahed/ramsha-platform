// using Ramsha.Application.Constants;
// using Ramsha.Application.Contracts;
// using Ramsha.Application.Contracts.Persistence;
// using Ramsha.Application.DTOs.Account.Responses;
// using Ramsha.Application.Extensions;
// using Ramsha.Application.Wrappers;
// using Ramsha.Domain.Customers.Entities;
// using Microsoft.AspNetCore.Http;
// using Ramsha.LocalMessaging;


// namespace Ramsha.Application.Features.Account.Commands.RefreshToken;

// internal class RefreshCommandHandler(
//     IBasketRepository basketRepository,
// 	IAccountServices accountServices,
// 	ICookieService cookieService)
// 	: RamshaCommandHandler<RefreshCommand>
// {
// 	public override async Task<IRamshaResult> HandleAsync(RefreshCommand request, CancellationToken cancellationToken)
// 	{
// 		var refreshToken = cookieService.GetCookieValue(ApplicationCookies.RefreshToken);
// 		if (refreshToken is null)
// 			return Unauthenticated(message: "no refresh token found in the cookie");



// 		var result = await accountServices.Refresh(refreshToken, request.AccessToken);
// 		if (!result.Succeeded)
// 			return result.Error;

// 		if (!string.IsNullOrEmpty(result.Value?.RefreshToken))
// 		{
// 			cookieService.SetCookieValue(ApplicationCookies.RefreshToken,
// 				result.Value.RefreshToken,
// 				new()
// 				{
// 					HttpOnly = true,
// 					Expires = result.Value.RefreshTokenExpiration.ToLocalTime(),
// 					SameSite = SameSiteMode.Lax,
// 					Secure = true,
// 				});
// 		}

// 		var dto = result.Value.Role switch
// 		{
// 			Roles.Customer => result.Value.AsAuthCustomerDto(await GetCustomerBasket(result.Value.Username)),
// 			_ => result.Value.AsAuthUserDto()
// 		};

// 		return Success(dto);

// 	}

// 	private async Task<Basket?> GetCustomerBasket(string username)
// 	{
// 		return await basketRepository.FindByBuyer(username);
// 	}
// }
