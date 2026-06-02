
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Commands.AddSupplierVariant;

public class AddSupplierVariantCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public Guid ProductVariantId { get; set; }
    public string? Description { get; set; }
    public decimal WholesalePrice { get; set; }
    public List<ImageRequest> VariantImagesToAdd { get; set; } = [];

}
