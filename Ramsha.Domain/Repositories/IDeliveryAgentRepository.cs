using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Common.Domain;
using Ramsha.Domain.DeliveryAgents;

namespace Ramsha.Domain.Repositories;

public interface IDeliveryAgentRepository : IRepository<DeliveryAgent, DeliveryAgentId>
{

}
