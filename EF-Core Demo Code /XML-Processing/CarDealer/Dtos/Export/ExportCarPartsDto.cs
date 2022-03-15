namespace CarDealer.Dtos.Export
{
    using System;
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportCarPartsDto
    {
        public ExportCarPartsDto()
        {
        }

        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public ExportCarPartsInfoDto[] Parts { get; set; }
    }

    [XmlType("part")]
    public class ExportCarPartsInfoDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
