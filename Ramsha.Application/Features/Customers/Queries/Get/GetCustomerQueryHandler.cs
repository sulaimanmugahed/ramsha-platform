using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Customers;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Customers;
using Ramsha.LocalMessaging;


namespace Ramsha.Application.Features.Customers.Queries.Get;

public class GetCustomerQueryHandler(IUserService userService, ICustomerRepository customerRepository)
: RamshaQueryHandler<GetCustomerQuery>
{
	public override async Task<IRamshaResult> HandleAsync(GetCustomerQuery request, CancellationToken cancellationToken)
	{
		var customer = await customerRepository
			.FindAsync(new CustomerId(request.Id));

		if (customer is null)
			return NotFound();


		var customerAccount = await userService.GetAccount(customer.Username);
		if (!customerAccount.Succeeded)
			return NotFound();

		return Success(new CustomerDto(
			customer.Id.Value,
			 customer.FirstName,
		 customer.LastName,
			 customerAccount.Value.Username
		));

	}

}
