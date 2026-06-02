



using Ramsha.Common.Domain;

namespace Ramsha.Domain.Orders.Entities;

public class OrderItem : Entity<int>
{
    public OrderItem(OrderId orderId, ProductItemOrdered itemOrdered, decimal price, int quantity, decimal markup)
    {
        ItemOrdered = itemOrdered;
        Price = price;
        Quantity = quantity;
        OrderId = orderId;
        Markup = markup;
    }

    public OrderItem()
    {

    }

    public ProductItemOrdered ItemOrdered { get; set; }
    public OrderId OrderId { get; set; }
    public FulfillmentRequestId FulfillmentRequestId { get; set; }

    public decimal Price { get; set; }
    public decimal Markup { get; set; }

    public int Quantity { get; set; }
}
