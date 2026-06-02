
using Ramsha.Application.Dtos.Common;
using Ramsha.Application.Wrappers;
using Ramsha.Application.Dtos.Products;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Products.Queries.GetProductOptions;

public class GetProductOptionsQuery : IRamshaQuery
{
    public Guid ProductId { get; set; }
}
