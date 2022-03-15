namespace CarDealer.Dtos2.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Part")]
    public class ImportPartDto2
    {
        public ImportPartDto2()
        {
        }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("quantity")]
        public int Quantity { get; set; }

        [XmlElement("supplierId")]
        public int SupplierId { get; set; }
    }
}
