using Ramsha.Common.Domain;
using Ramsha.Domain.Customers.Entities;
using Ramsha.Domain.Inventory;
using Ramsha.Domain.Inventory.Entities;


namespace Ramsha.Domain.Baskets.Entities;

public class BasketItem : Entity
{
	public int Quantity { get; set; }
	public InventoryItemId InventoryItemId { get; set; }
	public InventoryItem InventoryItem { get; set; }
	public BasketId BasketId { get; set; }
	public Basket Basket { get; set; }

	public override object GetId()
	{
		return new { InventoryItemId, BasketId };
	}
}
