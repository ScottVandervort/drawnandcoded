using System.Xml.Serialization;

namespace ScottsJewels.UnitTests
{
    public class OrderItem
    {
        [XmlAttribute]
        public string Sku { get; set; }
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Uri { get; set; }
        [XmlAttribute]
        public string Status { get; set; }
        [XmlAttribute]
        public decimal Price { get; set; }
        [XmlAttribute]
        public int Quantity { get; set; }
        [XmlAttribute]
        public decimal Shipping { get; set; }
        [XmlAttribute]
        public decimal Subtotal { get; set; }
        [XmlAttribute]
        public decimal SalesTax { get; set; }
        [XmlAttribute]
        public decimal GrandTotal { get; set; }
    }
}
