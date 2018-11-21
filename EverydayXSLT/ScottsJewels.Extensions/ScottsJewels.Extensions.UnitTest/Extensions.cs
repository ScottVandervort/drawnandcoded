using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ScottsJewels.UnitTests
{
    [TestClass]
    public class Extensions
    {
        private Order CreateOrder()
        {
            Order o = new Order
            {
                SaleDate = DateTime.Now,
                OrderNumber = 12345,
                SalesTax = 0.00m,
                Shipping = 0.00m,
                GrandTotal = 74.86m
            };

            o.Customer.FirstName = "Homer";
            o.Customer.LastName = "Simpson";
            o.Customer.Id = 12345;
            o.Customer.Email = "hsimpson@springfield.com";

            o.Addresses.Add(new Address
            {
                Type = "SHIP",
                FirstName = "Homer",
                LastName = "Simpson",
                Line1 = "742 Evergreen Terrace",
                Line2 = string.Empty,
                City = "Springfield",
                State = "IL",
                Zip = "62701"
            });

            o.Addresses.Add(new Address
            {
                Type = "BILL",
                FirstName = "Homer",
                LastName = "Simpson",
                Line1 = "742 Evergreen Terrace",
                Line2 = string.Empty,
                City = "Springfield",
                State = "IL",
                Zip = "62701"
            });
            o.OrderItems.Add(new OrderItem
            {
                Sku = "12345",
                Name = "White Collared Polo Shirt",
                Uri = "http://www.zappos.com/tommy-bahama-the-emfielder-polo-shirt-white",
                Status = "SHIP",
                Price = 29.99m,
                Quantity = 1,
                Shipping = 0.00m,
                Subtotal = 29.99m,
                SalesTax = 0.00m,
                GrandTotal = 29.99m
            });
            o.OrderItems.Add(new OrderItem
            {
                Sku = "12346",
                Name = "Blue Slacks",
                Uri = "http://www.zappos.com/adidas-climacool-3-stripes-pant-chuy-white",
                Status = "ONORD",
                Price = 32.99m,
                Quantity = 1,
                Shipping = 0.00m,
                Subtotal = 32.99m,
                SalesTax = 0.00m,
                GrandTotal = 32.99m
            });
            o.OrderItems.Add(new OrderItem
            {
                Sku = "12347",
                Name = "Doughnut w/ Spinkles",
                Uri = "http://www.amazon.com/Vo-Toys-Squeaky-Doughnut-Assorted-styles/dp/B0006G5HYM",
                Status = "ONORD",
                Price = .99m,
                Quantity = 12,
                Shipping = 0.00m,
                Subtotal = 11.88m,
                SalesTax = 0.00m,
                GrandTotal = 11.88m
            });

            return o;
        }

        [TestMethod]
        public void ToXml()
        {
            string xml = CreateOrder().ToXml();

            Console.WriteLine(xml);

            Assert.IsNotNull(xml);
            Assert.IsFalse(string.IsNullOrEmpty(xml));
        }

        [TestMethod]
        public void XslTransform_EmailOrderShipped ()
        {
            Order order = CreateOrder();
            
            string html = order.XslTransform(@"../../../../templates/emailOrderShipped.xsl");

            Console.WriteLine(html);

            Assert.IsNotNull(html);
            Assert.IsFalse(string.IsNullOrEmpty(html));                        
        }

        [TestMethod]
        public void XslTransform_EmailOrderBackordered()
        {
            Order order = CreateOrder();

            string html = order.XslTransform(@"../../../../templates/emailOrderBackordered.xsl");

            Console.WriteLine(html);

            Assert.IsNotNull(html);
            Assert.IsFalse(string.IsNullOrEmpty(html));
        }

        [TestMethod]
        public void XslTransform_EmailOrderPlaced()
        {
            Order order = CreateOrder();

            string html = order.XslTransform(@"../../../../templates/emailOrderPlaced.xsl");

            Console.WriteLine(html);

            Assert.IsNotNull(html);
            Assert.IsFalse(string.IsNullOrEmpty(html));
        }
    }
}
