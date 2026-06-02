using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.DeliveryAgents;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.DeliveryAgents.Queries.GetAllDeliveryAgents;

public class GetAllDeliveryAgentsQueryHandler(
    IDeliveryAgentRepository deliveryAgentRepository
) : RamshaQueryHandler<GetAllDeliveryAgentsQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetAllDeliveryAgentsQuery request, CancellationToken cancellationToken)
    {
        var agents = await deliveryAgentRepository.GetListAsync();

        return Success(agents.Select(x => new DeliveryAgentDto(x.Id.Value, x.Username)).ToList());
    }
}
