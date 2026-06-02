
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Dtos.Products;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.AddProductVariant;

public class AddProductVariantRangeCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public List<VariantCommand> Variants { get; set; }
}

