// using Ramsha.Application.Constants;
// using Ramsha.Application.Contracts;
// using Ramsha.Application.Contracts.Identity;
// using Ramsha.Application.Contracts.Persistence;
// using Ramsha.Application.Dtos.Account.Responses;
// using Ramsha.Application.DTOs.Account.Responses;
// using Ramsha.Application.Extensions;
// using Ramsha.Application.Wrappers;
// using Ramsha.Domain.Constants;
// using Ramsha.Domain.Customers.Entities;

// using Microsoft.AspNetCore.Http;
// using System.Net.Http;
// using Ramsha.LocalMessaging;


// namespace Ramsha.Application.Features.Account.Commands.Login;

// internal class LoginCommandHandler(
// 	IAccountServices accountServices,
// 	IBasketRepository basketRepository,
// 	ICookieService cookieService)
// 	: RamshaCommandHandler<LoginCommand>
// {
// 	public override async Task<IRamshaResult> HandleAsync(LoginCommand request, CancellationToken cancellationToken)
// 	{
// 		return await TransactionalUnitOfWork<IRamshaResult>(async () =>
// 		{
// 			var result = await accountServices.Authenticate(
// 						new() { UserName = request.Username, Password = request.Password });

// 			if (!result.Succeeded)
// 			{
// 				return result.Error;
// 			}

// 			if (!string.IsNullOrEmpty(result.Value?.RefreshToken))
// 			{
// 				cookieService.SetCookieValue(ApplicationCookies.RefreshToken,
// 					result.Value.RefreshToken,
// 					new()
// 					{
// 						HttpOnly = true,
// 						Expires = result.Value.RefreshTokenExpiration.ToLocalTime(),
// 						SameSite = SameSiteMode.Lax,
// 						Secure = true,
// 					});
// 			}


// 			var dto = result.Value!.Role switch
// 			{
// 				Roles.Customer => result.Value.AsAuthCustomerDto(await GetAndHandleCustomerBasket(result.Value.Username)),
// 				_ => result.Value.AsAuthUserDto()
// 			};

// 			return Success(dto);
// 		});


// 	}

// 	private async Task<Basket?> GetAndHandleCustomerBasket(string username)
// 	{
// 		var authCustomerBasket = await basketRepository.FindByBuyer(username);

// 		var anonCustomer = cookieService.GetCookieValue(ApplicationCookies.Buyer);
// 		var anonCustomerBasket = await basketRepository.FindByBuyer(anonCustomer);

// 		if (anonCustomerBasket is not null)
// 		{
// 			if (authCustomerBasket is not null)
// 			{
// 				await basketRepository.DeleteAsync(authCustomerBasket);
// 			}
// 			anonCustomerBasket.Buyer = username;
// 			cookieService.RemoveCookie(ApplicationCookies.Buyer);
// 		}

// 		return anonCustomerBasket ?? authCustomerBasket;
// 	}
// }
