
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Customers.Entities;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Customers.Queries.GetCurrentCustomerAddress;

public class GetCurrentCustomerAddressQueryHandler(
    ICustomerRepository customerRepository,
    IAuthenticatedUserService authenticatedUserService
) : RamshaQueryHandler<GetCurrentCustomerAddressQuery>
{
    public override async Task<IRamshaResult> HandleAsync(GetCurrentCustomerAddressQuery request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.FindAsync(x => x.Username == authenticatedUserService.UserName, [x => x.Address]);
        if (customer is null)
            return Unauthenticated();

        return Success(customer.Address);

    }
}
