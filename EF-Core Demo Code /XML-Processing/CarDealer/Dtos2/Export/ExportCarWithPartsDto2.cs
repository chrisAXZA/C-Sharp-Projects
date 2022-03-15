namespace CarDealer.Dtos2.Export
{
    using System;
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportCarWithPartsDto2
    {
        public ExportCarWithPartsDto2()
        {
        }

        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        public PartCarDetailDto2[] Parts { get; set; }

    }

    [XmlType("part")]
    public class PartCarDetailDto2
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}
