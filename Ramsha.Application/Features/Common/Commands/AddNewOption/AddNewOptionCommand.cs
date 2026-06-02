
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Common.Commands.AddNewOption;

public class AddNewOptionCommand : IRamshaCommand
{
    public string OptionName { get; set; }
    public List<string> OptionValues { get; set; } = [];
}
