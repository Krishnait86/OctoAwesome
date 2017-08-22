using System.Xml.Serialization;

namespace OctoAwesome.Model
{
    [XmlInclude(typeof(TreeItem))]
    public abstract class Item
    {
        public Vector2 Position { get; set; }
    }
}
