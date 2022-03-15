namespace ProductShop.Dtos.Export
{
    using System;
    using System.Xml.Serialization;
    
    using ProductShop.Models;

    [XmlType("User")]
    public class ExportUserCollectionDto
    {
        public ExportUserCollectionDto()
        {
        }

        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public ExportProductCollectionDto SoldProducts { get; set; }
    }
}
