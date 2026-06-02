using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Dtos.Customers;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Customers.Queries.GetCustomersPaged;

public class GetCustomersPagedQuery : PagedParams, IRamshaQuery
{

}
