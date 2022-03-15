namespace ProductShop.Dtos.Export
{
    using System;
    using System.Xml.Serialization;

    using ProductShop.Models;

    [XmlType("Users")]
    public class ExportUserProductsDto
    {
        public ExportUserProductsDto()
        {
        }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public ExportUserCollectionDto[] Users { get; set; }
    }
}
