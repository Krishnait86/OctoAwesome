using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace OctoAwesome.Model
{
    [XmlInclude(typeof(BoxItem))]
    [XmlInclude(typeof(TreeItem))]
    public abstract class Item
    {
        public Vector2 Position { get; set; }
    }
}
