using System.Xml.Serialization;

namespace ScottsJewels.UnitTests
{
    public class Address
    {
        [XmlAttribute]
        public string Type { get; set; }
        [XmlAttribute]
        public string FirstName { get; set; }
        [XmlAttribute]
        public string LastName { get; set; }
        [XmlAttribute]
        public string Line1 { get; set; }
        [XmlAttribute]
        public string Line2 { get; set; }
        [XmlAttribute]
        public string City { get; set; }
        [XmlAttribute]
        public string State { get; set; }
        [XmlAttribute]
        public string Zip { get; set; }
    }
}
