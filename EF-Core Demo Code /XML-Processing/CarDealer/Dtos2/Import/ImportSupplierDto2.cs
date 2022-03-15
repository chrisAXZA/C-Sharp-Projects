namespace CarDealer.Dtos2.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Supplier")]
    public class ImportSupplierDto2
    {
        public ImportSupplierDto2()
        {
        }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImporter")]
        public bool IsImporter { get; set; }
    }
}
