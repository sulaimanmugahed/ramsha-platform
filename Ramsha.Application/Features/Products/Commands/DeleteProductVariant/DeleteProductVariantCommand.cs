
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.DeleteProductVariant;

public class DeleteProductVariantCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }
}
