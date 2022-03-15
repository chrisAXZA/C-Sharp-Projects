namespace ProductShop.Dtos.Export
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Product")]
    public class ExportProductDetailsDto
    {
        public ExportProductDetailsDto()
        {
        }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
