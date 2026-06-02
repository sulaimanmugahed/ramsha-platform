
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Common.Commands.RemoveOption;

public class RemoveOptionCommand : IRamshaCommand
{
    public Guid OptionId { get; set; }
}
