using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Constants;
using Ramsha.LocalMessaging;

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
