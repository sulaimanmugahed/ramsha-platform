
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Account.Requests;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Constants;
using Ramsha.Domain.DeliveryAgents;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.DeliveryAgents.Commands.CreateDeliveryAgent;

public class CreateDeliveryAgentCommandHandler(
IDeliveryAgentRepository deliveryAgentRepository,
IUserService userService
) : RamshaCommandHandler<CreateDeliveryAgentCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateDeliveryAgentCommand request, CancellationToken cancellationToken)
    {
        var registerResult = await userService.CreateAccount(new RegisterRequest
        {
            Email = request.Email,
            Password = request.Password,
            Username = request.Username

        }, Roles.DeliveryAgent);

        if (!registerResult.Succeeded)
        {
            return registerResult.Error;
        }

        var customer = DeliveryAgent.Create(
            request.Username,
            request.FirstName,
            request.LastName);

        await deliveryAgentRepository.AddAsync(customer);

        return Success(customer.Id.Value.ToString());
    }
}
