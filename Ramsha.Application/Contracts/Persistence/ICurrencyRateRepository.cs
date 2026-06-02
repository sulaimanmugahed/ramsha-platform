using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Common.Domain;
using Ramsha.Domain.Common;

namespace Ramsha.Application.Contracts.Persistence;

public interface ICurrencyRateRepository : IRepository<Currency, int>
{

}

