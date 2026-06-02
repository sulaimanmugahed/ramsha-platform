
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.DeleteProductOption;

public class DeleteProductOptionCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public Guid OptionId { get; set; }
}
