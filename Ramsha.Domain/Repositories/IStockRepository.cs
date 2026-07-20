using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Common.Domain;
using Ramsha.Domain.Inventory;
using Ramsha.Domain.Inventory.Entities;

namespace Ramsha.Domain.Repositories;

public interface IStockRepository : IRepository<Stock, StockId>
{

}
