using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ScottsJewels.UnitTests
{
    public class Order
    {
        [XmlAttribute]
        public DateTime SaleDate { get; set; }
        [XmlAttribute]
        public int OrderNumber { get; set; }
        [XmlAttribute]
        public decimal SalesTax { get; set; }
        [XmlAttribute]
        public decimal Shipping { get; set; }
        [XmlAttribute]
        public decimal GrandTotal { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Address> Addresses { get; set; }
        public Customer Customer { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
            Addresses = new List<Address>();
            Customer = new Customer();
        }
    }
}
