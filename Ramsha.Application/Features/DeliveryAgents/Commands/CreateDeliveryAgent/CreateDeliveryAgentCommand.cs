
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.DeliveryAgents.Commands.CreateDeliveryAgent;

public class CreateDeliveryAgentCommand : IRamshaCommand
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
