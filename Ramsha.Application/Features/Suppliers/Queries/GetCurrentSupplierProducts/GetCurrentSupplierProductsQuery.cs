
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Suppliers.Queries.GetCurrentSupplierProducts;

public class GetCurrentSupplierProductsQuery : IRamshaQuery
{
    public Ramsha.Application.Wrappers.PaginationParams PaginationParams { get; set; }
}
