
using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Domain.Orders.Events;
using Ramsha.Domain.Suppliers.Entities;
using Ramsha.LocalMessaging.Abstractions;

namespace Ramsha.Application.Features.Orders.EventsHandler;

public class NewOrderCreatedEventHandler : ILocalEventHandler<NewOrderCreatedEvent>
{
    public async Task HandleAsync(NewOrderCreatedEvent message, CancellationToken cancellationToken)
    {
        // var orderItemsGroups = notification.OrderItems.GroupBy(x => x.ItemOrdered.SupplierId);
        // foreach (var itemsGroup in orderItemsGroups)
        // {
        //     var supplier = await supplierRepository.GetByIdAsync(itemsGroup.Key);
        //     if (supplier is null)
        //         return;

        //     var fulfillmentItems = itemsGroup.Select(item => new FulfillmentRequestItem
        //     {
        //         InventoryItemId = item.ItemOrdered.InventoryItemId,
        //         Quantity = item.Quantity,
        //         ImageUrl = item.ItemOrdered.PictureUrl,
        //         Name = item.ItemOrdered.Name,
        //         Price = item.Price,
        //         Sku = item.ItemOrdered.InventorySKU
        //     }).ToList();

        //     supplier.AddFulfillmentRequest(notification.OrderId, fulfillmentItems);
        // }
        // await unitOfWork.SaveChangesAsync();
    }
}
