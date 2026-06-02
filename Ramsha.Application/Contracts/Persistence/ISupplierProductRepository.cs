
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.DTOs.Common;
using Ramsha.Application.Wrappers;
using Ramsha.Common.Domain;
using Ramsha.Domain.Common;
using Ramsha.Domain.Products;
using Ramsha.Domain.Products.Entities;
using Ramsha.Domain.Suppliers;
using Ramsha.Domain.Suppliers.Entities;

namespace Ramsha.Application.Contracts.Persistence;

public interface ISupplierProductRepository : IRepository<SupplierProduct>
{
    Task<PaginationResponseDto<SupplierProductDto>> GetPaged(SupplierId supplierId, Ramsha.Application.Wrappers.PaginationParams paginationParams);
    Task<List<SupplierVariant>> GetVariantList(SupplierId supplierId, ProductId productId);
    Task<SupplierVariant?> GetVariantDetail(SupplierId supplierId, ProductId productId, ProductVariantId productVariantId);
    Task<SupplierVariant?> GetVariant(SupplierId supplierId, ProductId productId, ProductVariantId productVariantId);


}
