
using Ramsha.Application.Dtos.Admins;
using Ramsha.Application.Services.Identity.UserInterfaces;
using Ramsha.LocalMessaging;
using Ramsha.Shared;

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
