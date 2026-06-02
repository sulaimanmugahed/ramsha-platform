
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Dtos.Suppliers;
using Ramsha.Application.Extensions;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Suppliers.Entities;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Suppliers.Commands.AddSupplyRequestItem;

public class AddSupplyRequestItemCommandHandler(
    ISupplyRequestRepository supplyRequestRepository,
    ISupplierRepository supplierRepository,
    IAuthenticatedUserService authenticatedUserService,
    ISupplierProductRepository supplierProductRepository
) : RamshaCommandHandler<AddOrUpdateSupplyRequestItemCommand>
{
    public override async Task<IRamshaResult> HandleAsync(AddOrUpdateSupplyRequestItemCommand request, CancellationToken cancellationToken)
    {

        var supplier = await supplierRepository.FindAsync(x => x.Username == authenticatedUserService.UserName);
        if (supplier is null)
        {
            return Unauthenticated();
        }

        var productId = new Domain.Products.ProductId(request.ProductId);
        var variantId = new Domain.Products.ProductVariantId(request.ProductVariantId);

        var supplyRequest = await supplyRequestRepository.FindAsync(
            x => x.Supplier == supplier.Username,
            [x => x.Items]);

        if (supplyRequest is null)
        {
            supplyRequest = SupplyRequest.Create(supplier.Username);
            await supplyRequestRepository.AddAsync(supplyRequest, true);
        }

        var supplierProduct = await supplierProductRepository.FindAsync(
            x => x.ProductId == productId && x.SupplierId == supplier.Id,
             [p => p.SupplierVariants]);

        if (supplierProduct is null)
        {
            return Invalid(message: "no supplier product found");
        }


        var variant = supplierProduct.SupplierVariants.First(x => x.ProductVariantId == variantId);
        if (variant is null)
            return Invalid(message: "no variant found");

        var existItem = supplyRequest.Items
        .FirstOrDefault(x => x.ProductVariantId == variantId && x.ProductId.Value == request.ProductId);
        if (existItem is not null)
        {
            existItem.SetQuantity(request.Quantity);
        }
        else
        {
            var supplyRequestItem = SupplyRequestItem.Create(
                productId,
                variantId,
                supplier.Id,
                request.Quantity);

            supplyRequest.AddItem(supplyRequestItem);
        }

        return Success(supplyRequest.AsSupplyRequestDto());
    }
}
