
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Customers.Queries.Get;

public class GetCustomerQuery : IRamshaQuery
{
	public Guid Id { get; set; }
}
