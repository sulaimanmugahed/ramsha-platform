using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Application.Dtos.Admins;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Constants;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Admin.Queries.GetAdmins;

public class GetAdminsQueryHandler(
    IUserService userService
) : RamshaQueryHandler<GetAdminsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetAdminsQuery request, CancellationToken cancellationToken)
    {
        var response = await userService.GetAccounts(Roles.Admin);
        if (!response.Succeeded)
        {
            return response.Error;
        }

        return Success(response.Value?.Select(x => new AdminDto(x.Id, x.Username, x.Email)).ToList());
    }
}
