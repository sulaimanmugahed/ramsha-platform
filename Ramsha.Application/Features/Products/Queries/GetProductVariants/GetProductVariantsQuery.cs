
using Ramsha.Application.Dtos.Products;
using Ramsha.Application.Wrappers;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Queries.GetProductVariants;

public class GetProductVariantsQuery : IRamshaQuery
{
    public Guid ProductId { get; set; }

}
