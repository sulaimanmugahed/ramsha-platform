
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Tags.Commands.UpdateTag;

public class UpdateTagCommand : IRamshaCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
