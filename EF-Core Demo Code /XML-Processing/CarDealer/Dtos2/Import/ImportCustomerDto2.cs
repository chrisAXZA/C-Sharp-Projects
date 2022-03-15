namespace CarDealer.Dtos2.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Customer")]
    public class ImportCustomerDto2
    {
        public ImportCustomerDto2()
        {
        }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("birthDate")]
        public string BirthDate { get; set; }

        [XmlElement("isYoungDriver")]
        public bool IsYoungDriver { get; set; }
    }
}
