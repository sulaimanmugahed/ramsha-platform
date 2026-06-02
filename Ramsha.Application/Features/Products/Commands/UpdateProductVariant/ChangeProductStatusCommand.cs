
using Ramsha.Domain.Products.Enums;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.UpdateProductVariant;

public class ChangeProductStatusCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public ProductStatus Status { get; set; }
}
