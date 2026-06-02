
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRamshaCommand
{
    public Guid ProductId { get; set; }
    public UpdateProductBasicCommand? ProductBasicCommand { get; set; }
    public UpdateProductAdditionalCommand? ProductAdditionalCommand { get; set; }
}
