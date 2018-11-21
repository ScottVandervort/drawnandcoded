using System.Xml.Serialization;

namespace ScottsJewels.UnitTests
{
    public class Customer
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string FirstName { get; set; }
        [XmlAttribute]
        public string LastName { get; set; }
        [XmlAttribute]
        public string Email { get; set; }
    }

}
