using System.Collections.Generic;

namespace OctoAwesome.Model
{
    interface IHaveInventory
    {
        List<InventoryItem> InventoryItems { get; }
    }
}
