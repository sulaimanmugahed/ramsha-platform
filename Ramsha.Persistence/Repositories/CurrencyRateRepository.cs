using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Common;
using Ramsha.EntityFrameworkCore;
using Ramsha.Persistence.Contexts;

namespace Ramsha.Persistence.Repositories;

public class CurrencyRateRepository : EFCoreRepository<ApplicationDbContext, Currency, int>, ICurrencyRateRepository
{

}
