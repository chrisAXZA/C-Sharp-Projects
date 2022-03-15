namespace ProductShop.Dtos.Export
{
    using System;
    using System.Xml.Serialization;
    
    using ProductShop.Models;

    [XmlType("SoldProducts")]
    public class ExportProductCollectionDto
    {
        public ExportProductCollectionDto()
        {
        }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public ExportProductDetailsDto[] Products { get; set; }
    }
}
