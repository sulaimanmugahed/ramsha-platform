
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.DeliveryAgents;
using Ramsha.EntityFrameworkCore;
using Ramsha.Persistence.Contexts;

namespace Ramsha.Persistence.Repositories;

public class DeliveryAgentRepository
: EFCoreRepository<ApplicationDbContext, DeliveryAgent, DeliveryAgentId>,
IDeliveryAgentRepository
{

}
