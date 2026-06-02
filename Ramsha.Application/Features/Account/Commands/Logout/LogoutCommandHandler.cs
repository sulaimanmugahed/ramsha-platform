
using Ramsha.Application.Constants;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Identity;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Account.Commands.Logout;

public class LogoutCommandHandler(
    IAccountServices accountServices,
    ICookieService cookieService
) : RamshaCommandHandler<LogoutCommand>
{
    public override async Task<IRamshaResult> HandleAsync(LogoutCommand request, CancellationToken cancellationToken)
    {
        var token = cookieService.GetCookieValue(ApplicationCookies.RefreshToken);
        if (token is null)
            return Unauthenticated(message: "empty cookie");

        var result = await accountServices.Revoke(token);

        if (result.Succeeded)
            cookieService.RemoveCookie(ApplicationCookies.RefreshToken);

        await accountServices.LogoutCurrentUser();

        return Success();
    }
}
