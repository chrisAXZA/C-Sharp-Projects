namespace CarDealer.Dtos2.Export
{
    using System;
    using System.Xml.Serialization;

    [XmlType("suplier")]
    public class ExportLocalSupplierDto2
    {
        public ExportLocalSupplierDto2()
        {
        }

        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("parts-count")]
        public int PartsCount { get; set; }
    }
}
