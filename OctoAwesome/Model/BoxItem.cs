using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctoAwesome.Model
{
    public class BoxItem : Item, IHaveInventory
    {
        public List<InventoryItem> InventoryItems { get; private set; }

        public BoxItem()
        {
            InventoryItems = new List<InventoryItem>();

            if (!new StreamReader(@"Assets\test10.map").ReadToEnd().Contains("Diamant"))
            {
                InventoryItems.Add(new InventoryItem() { Name = "Diamant" });
            }
            return;
        }
    }
}
