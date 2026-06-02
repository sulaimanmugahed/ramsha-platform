
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Products.Entities;

using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRamshaCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid Category { get; set; }
    public Guid Brand { get; set; }
    public List<string>? Tags { get; set; }
    public string? ImageUrl { get; set; }
    public SeoSettings? SeoSettings { get; set; }
    public VariantCommand DefaultVariant { get; set; }
    public List<ProductOptionCommand> Options { get; set; }
}
