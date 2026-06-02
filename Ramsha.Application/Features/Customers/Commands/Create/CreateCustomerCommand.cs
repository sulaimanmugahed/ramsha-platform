
using Ramsha.Domain.Products.Enums;
using Ramsha.LocalMessaging.Abstractions;


namespace Ramsha.Application.Features.Customers.Commands.Create;

public class CreateCustomerCommand : IRamshaCommand
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
	public CurrencyCode PreferredCurrency { get; set; }



}
