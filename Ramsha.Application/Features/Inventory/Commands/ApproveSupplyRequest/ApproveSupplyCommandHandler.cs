using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Inventory.Entities;
using Ramsha.Domain.Suppliers.Enums;

using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Inventory.Commands.ApproveSupplyRequest;

public class ApproveSupplyCommandHandler(
    ISupplyRepository supplyRepository,
    IInventoryItemRepository inventoryItemRepository,
    ISupplierProductRepository supplierProductRepository,
    ISupplierRepository supplierRepository,
    IProductRepository productRepository
) : RamshaCommandHandler<ApproveSupplyCommand>
{
    public override async Task<IRamshaResult> HandleAsync(ApproveSupplyCommand request, CancellationToken cancellationToken)
    {
        return await TransactionalUnitOfWork<IRamshaResult>(async () =>
                {
                    var supply = await supplyRepository.GetWithDetails(x => x.Id == new Domain.Suppliers.SupplyId(request.SupplyId));
                    if (supply is null)
                        return NotFound(message: "no supply exist");

                    if (supply.Items.Count == 0)
                        return Invalid(message: "The request has no item");

                    var supplier = await supplierRepository.FindAsync(x => x.Username == supply.Supplier);
                    if (supplier is null)
                    {
                        return NotFound(message: "no supplier exist");
                    }

                    foreach (var item in supply.Items)
                    {
                        var product = await productRepository.FindAsync(item.ItemSupplied.ProductId);
                        if (product is null)
                            return NotFound(message: "no product found with in the item");

                        var supplierVariant = await supplierProductRepository.GetVariant(
                supplier.Id, product.Id, item.ItemSupplied.ProductVariantId);

                        if (supplierVariant is null)
                            return Invalid(message: "no supplier Variant found with in the item");

                        var price = new Domain.Common.Price(item.WholesalePrice, supply.Currency);

                        var inventoryItem = await inventoryItemRepository.FindAsync(x =>
                    x.ProductId == item.ItemSupplied.ProductId &&
                    x.SupplierId == supplier.Id &&
                    x.ProductVariantId == item.ItemSupplied.ProductVariantId,
                   [x => x.Stocks]);

                        if (inventoryItem is null)
                        {
                            inventoryItem = InventoryItem.Create(
                        item.ItemSupplied.ProductId,
                        item.ItemSupplied.ProductVariantId,
                        supplier.Id,
                        item.ItemSupplied.Sku,
                        product.Name
                    );
                            await inventoryItemRepository.AddAsync(inventoryItem);
                        }

                        inventoryItem.AddStock(item.Quantity, price);
                    }

                    supply.SetStatus(SupplyStatus.Approved);

                    return Success();
                });
    }
}