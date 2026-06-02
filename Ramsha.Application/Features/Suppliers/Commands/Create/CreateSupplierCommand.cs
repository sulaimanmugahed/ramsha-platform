
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Commands.Create;

public class CreateSupplierCommand : IRamshaCommand
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string Username { get; set; }
	public string Password { get; set; }
}
