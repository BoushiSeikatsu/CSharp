using System.Xml.Serialization;

namespace ASPNETPractice.Models
{
    [XmlRoot("CATALOG")]
    public class Catalog
    {
        [XmlElement("CD")]
        public List<CD> CDs { get; set; }
    }
}
