
using Ramsha.Application.Services.Identity.UserInterfaces;
using Ramsha.LocalMessaging;
using Ramsha.Shared;

namespace Ramsha.Application.Features.Admin.Commands.CreateAdmin;

public class CreateAdminCommandHandler(
    IUserService userService
) : RamshaCommandHandler<CreateAdminCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.CreateAccount(new Dtos.Account.Requests.RegisterRequest
        {
            Email = request.Email,
            Password = request.Password,
            Username = request.Username
        }, Roles.Admin, request.Permissions, true, true);

        if (!result.Succeeded)
        {
            return result.Error;
        }

        return Success(result.Value?.Id.ToString());
    }
}
