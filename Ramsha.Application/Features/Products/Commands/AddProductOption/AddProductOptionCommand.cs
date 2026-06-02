
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.AddProductOption;

public class AddProductOptionCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public Guid OptionId { get; set; }
    public int Priority { get; set; }
}
