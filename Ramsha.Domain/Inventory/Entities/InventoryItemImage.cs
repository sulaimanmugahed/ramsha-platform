using Ramsha.Common.Domain;

namespace Ramsha.Domain.Inventory.Entities;

public class InventoryItemImage : Entity<int>
{
    public InventoryItemId InventoryItemId { get; set; }
    public InventoryItem InventoryItem { get; set; }
    public string Url { get; set; }
    public string Path { get; set; }
    public bool IsHome { get; set; }
}
