using Ramsha.Application.Constants;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Identity;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;


namespace Ramsha.Application.Features.Account.Commands.RevokeToken;

public class RevokeCommandHandler(
	IAccountServices accountServices,
	ICookieService cookieService)
	: RamshaCommandHandler<RevokeCommand>
{
	public override async Task<IRamshaResult> HandleAsync(RevokeCommand request, CancellationToken cancellationToken)
	{
		var token = cookieService.GetCookieValue(ApplicationCookies.RefreshToken);
		if (token is null)
			return Unauthenticated(message: "no refresh token in cookie");
		var result = await accountServices.Revoke(token);

		if (result.Succeeded)
			cookieService.RemoveCookie(ApplicationCookies.RefreshToken);

		return result;
	}
}
