using Ramsha.Application.Contracts;
using Ramsha.Application.Contracts.Persistence;
using Ramsha.Application.Wrappers;
using Ramsha.Domain.Orders.Entities;

using Ramsha.Application.Services;
using Ramsha.Domain.Baskets.Entities;
using Ramsha.Application.Contracts.Identity.UserInterfaces;
using Ramsha.Domain.Common;
using Ramsha.LocalMessaging;

namespace Ramsha.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
    IBasketRepository basketRepository,
    IGeocodingService geocodingService,
    IUserService userService,
    ICustomerRepository customerRepository,
    IAuthenticatedUserService authenticatedUser,
    IOrderRepository orderRepository,
    DeliveryFeeService deliveryFeeService
) : RamshaCommandHandler<CreateOrderCommand>
{
    public override async Task<IRamshaResult> HandleAsync(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        var customer = await customerRepository
        .FindAsync(x => x.Username == authenticatedUser.UserName);

        if (customer is null)
            return Unauthenticated();

        var shippingAddress = request.ShippingAddress;
        if (shippingAddress is null)
        {
            var customerAddress = await userService.GetUserAddress(authenticatedUser.UserName);
            if (customerAddress is null)
            {
                return Invalid(message: "Customer address is null");
            }
            shippingAddress = new ShippingAddress
            {
                City = customerAddress.City,
                Country = customerAddress.Country,
                Description = customerAddress.Description,
                Display = customerAddress.Display,
                FullName = customerAddress.FullName,
                Latitude = customerAddress.Latitude,
                Longitude = customerAddress.Longitude,
                State = customerAddress.State,
                Zip = customerAddress.Zip
            };
        }

        var basket = await basketRepository.GetDetail(authenticatedUser.UserName);
        if (basket is null || basket.PaymentIntentId is null)
            return Invalid(message: "basket is null");

        var order = Order.Create(customer.Id, basket.PaymentIntentId, shippingAddress);


        var itemsGroups = basket.Items.GroupBy(x => x.InventoryItem.Supplier);


        List<FulfillmentRequest> fulfillmentRequests = [];

        foreach (var group in itemsGroups)
        {
            var supplier = group.Key;
            var supplierAddress = await userService.GetUserAddress(supplier.Username);
            if (supplierAddress is null)
            {
                throw new Exception("supplier should has address");
            }

            var supplierCoordinates = (supplierAddress.Latitude, supplierAddress.Longitude);
            if (supplierAddress is null)
            {
                throw new Exception("supplier should has address");
            }

            decimal fulfillmentFee = 0;
            List<OrderItem> orderItems = [];
            foreach (var supplierItem in group)
            {

                var inventoryItem = supplierItem.InventoryItem;
                if (inventoryItem is null)
                    return Invalid(message: "one or more item was not found");

                var itemOrdered = new ProductItemOrdered(
                    inventoryItem.Id,
                    inventoryItem.ProductName,
                    inventoryItem.InventorySKU,
                    inventoryItem.ImageUrl
                );

                var orderItem = new OrderItem(order.Id, itemOrdered, inventoryItem.FinalPrice.Amount, supplierItem.Quantity, inventoryItem.RetailPrice.Amount - inventoryItem.WholesalePrice.Amount);
                orderItems.Add(orderItem);
                inventoryItem.DecreaseQuantity(supplierItem.Quantity);

                var itemWight = supplierItem.InventoryItem.ProductVariant.CalculateShippingWeight(supplierItem.Quantity);
                var distance = geocodingService.CalculateDistance(supplierCoordinates, (shippingAddress.Latitude, shippingAddress.Longitude));
                var deliveryFee = deliveryFeeService.CalculateDeliveryFee(itemWight, distance);
                fulfillmentFee += deliveryFee;
            }
            var fulfillmentRequest = FulfillmentRequest.Create(supplier.Id, orderItems, fulfillmentFee);
            fulfillmentRequests.Add(fulfillmentRequest);
        }

        order.SetFulfillmentRequests(fulfillmentRequests);

        await orderRepository.AddAsync(order);
        await basketRepository.DeleteAsync(basket);



        return Success(order.Id.Value.ToString());
    }
}
