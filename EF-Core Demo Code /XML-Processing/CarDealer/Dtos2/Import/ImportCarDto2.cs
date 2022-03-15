namespace CarDealer.Dtos2.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Car")]
    public class ImportCarDto2
    {
        public ImportCarDto2()
        {
        }

        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TraveledDistance { get; set; }

        [XmlArray("parts")]
        public ImportPart2[] Parts { get; set; }

    }

    [XmlType("partId")]
    public class ImportPart2
    {
        [XmlAttribute("id")]
        public int PartId { get; set; }
    }
}
