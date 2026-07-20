// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Ramsha.Application.Contracts;
// using Ramsha.Application.Services.Identity.UserInterfaces;
// using Ramsha.Application.Wrappers;
// using Ramsha.Contracts;
// using Ramsha.Domain.Common;
// using Ramsha.LocalMessaging;

// namespace Ramsha.Application.Features.Account.Commands.UpdateAddress;

// public class UpdateAddressCommandHandler(
//     IUserService userService,
//     IAuthenticatedUserService authenticatedUserService
// ) : RamshaCommandHandler<UpdateAddressCommand>
// {
//     public override async Task<IRamshaResult> HandleAsync(UpdateAddressCommand request, CancellationToken cancellationToken)
//     {
//         await userService.UpdateUserAddress(authenticatedUserService.UserName, request);
//         return Success();
//     }
// }
