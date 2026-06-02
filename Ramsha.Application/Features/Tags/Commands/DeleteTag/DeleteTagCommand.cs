
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Tags.Commands.DeleteTag;

public class DeleteTagCommand : IRamshaCommand
{
    public Guid Id { get; set; }
}
