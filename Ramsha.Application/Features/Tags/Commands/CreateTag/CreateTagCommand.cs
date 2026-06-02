
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Tags.Commands.CreateTag;

public class CreateTagCommand : IRamshaCommand
{
    public string Name { get; set; }
}
