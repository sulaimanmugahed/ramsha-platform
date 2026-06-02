using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Customers;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Customers.Queries.GetCustomersPaged;

public class GetCustomersPagedQueryHandler(
ICustomerRepository customerRepository,
IHttpService httpService
) : RamshaQueryHandler<GetCustomersPagedQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCustomersPagedQuery request, CancellationToken cancellationToken)
    {
        var response = await customerRepository.GetPaged(request);

        httpService.AddPagedHeader(response.MetaData);
        return Success(response.Data);
    }
}
