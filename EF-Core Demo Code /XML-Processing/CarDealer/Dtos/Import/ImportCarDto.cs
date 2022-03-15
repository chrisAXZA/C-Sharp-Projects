namespace CarDealer.Dtos.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class ImportCarDto
    {
        public ImportCarDto()
        {
        }
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public ImportCarPartDto[] Parts { get; set; }
    }

    [XmlType("partId")]
    public class ImportCarPartDto
    {
        [XmlAttribute("id")]
        public int PartId { get; set; }
    }
}
