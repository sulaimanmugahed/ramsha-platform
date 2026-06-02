using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Account.Requests;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Constants;
using Ramsha.Domain.Customers.Entities;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Customers.Commands.Create;

internal class CreateCustomerCommandHandler(
	IUserService userService,
	ICustomerRepository customerRepository)
	: RamshaCommandHandler<CreateCustomerCommand>
{
	public override async Task<IRamshaResult> HandleAsync(CreateCustomerCommand request, CancellationToken cancellationToken)
	{
		var registerResult = await userService.CreateAccount(new RegisterRequest
		{
			Email = request.Email,
			Password = request.Password,
			Username = request.Username,
			PreferredCurrency = request.PreferredCurrency

		}, roleName: Roles.Customer);

		if (!registerResult.Succeeded)
		{
			return registerResult.Error;
		}

		var customer = Customer.Create(
			request.Username,
			request.FirstName,
			request.LastName);

		await customerRepository.AddAsync(customer);


		return Success(customer.Id.Value.ToString());
	}
}