using System.Xml.Serialization;

namespace ASPNETPractice.Models
{
    public class CD
    {
        public int? Id { get; set; }
        [XmlElement("TITLE")]
        public string Title { get; set; }
        [XmlElement("ARTIST")]
        public string Artist { get; set; }
        [XmlElement("COUNTRY")]
        public string Country { get; set; }
        [XmlElement("COMPANY")]
        public string Company { get; set; }
        [XmlElement("PRICE")]
        public float Price { get; set; }
        [XmlElement("YEAR")]
        public int Year { get; set; }
    }
}
