using Ramsha.Application.Dtos.Account.Responses;
using Ramsha.Application.Dtos.Baskets;
using Ramsha.Application.DTOs.Account.Responses;
using Ramsha.Domain.Common;


namespace Ramsha.Api.Extensions;

public static class UserExtensions
{
	public static AuthenticatedCustomerDto AsAuthCustomerDto(this AuthenticationResult authenticationResult, BasketDto? basket)
	{
		return new AuthenticatedCustomerDto
		{
			AccessToken = authenticationResult.AccessToken,
			IsVerified = authenticationResult.IsVerified,
			Email = authenticationResult.Email,
			Username = authenticationResult.Username,
			Role = authenticationResult.Role,
			RefreshTokenExpiration = authenticationResult.RefreshTokenExpiration,
			Basket = basket,
			Address = authenticationResult.Address,
			PreferredCurrency = authenticationResult.PreferredCurrency

		};

	}
	public static AuthenticatedUserDto AsAuthUserDto(this AuthenticationResult authenticationResponse)
	{
		return new AuthenticatedUserDto
		{
			Email = authenticationResponse.Email,
			Username = authenticationResponse.Username,
			Role = authenticationResponse.Role,
			IsVerified = authenticationResponse.IsVerified,
			AccessToken = authenticationResponse.AccessToken,
			RefreshTokenExpiration = authenticationResponse.RefreshTokenExpiration,
			Address = authenticationResponse.Address,
			PreferredCurrency = authenticationResponse.PreferredCurrency
		};
	}
}
