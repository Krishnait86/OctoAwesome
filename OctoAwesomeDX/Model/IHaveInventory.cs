using System.Collections.Generic;

namespace OctoAwesome.Model
{
    public interface IHaveInventory
    {
        List<InventoryItem> InventoryItems { get; }
    }
}
