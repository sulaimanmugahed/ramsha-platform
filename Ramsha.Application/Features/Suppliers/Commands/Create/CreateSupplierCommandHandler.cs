using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Account.Requests;
using Ramsha.Application.Features.Suppliers.Commands.Create;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Constants;
using Ramsha.Domain.Customers.Entities;
using Ramsha.Domain.Suppliers.Entities;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Customers.Commands.Create;

internal class CreateSupplierCommandHandler(
	IUserService userService,
	ISupplierRepository supplierRepository)
	: RamshaCommandHandler<CreateSupplierCommand>
{
	public override async Task<IRamshaResult> HandleAsync(CreateSupplierCommand request, CancellationToken cancellationToken)
	{
		var registerResult = await userService.CreateAccount(new RegisterRequest
		{
			Email = request.Email,
			Password = request.Password,
			Username = request.Username
		}, Roles.Supplier);

		if (!registerResult.Succeeded)

		{
			return registerResult.Error;
		}

		var supplier = Supplier.Create(
			request.Username,
			request.FirstName,
			request.LastName);

		await supplierRepository.AddAsync(supplier);


		return Success(supplier.Id.Value.ToString());
	}
}