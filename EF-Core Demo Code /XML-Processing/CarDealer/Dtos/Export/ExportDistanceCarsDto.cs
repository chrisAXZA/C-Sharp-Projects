﻿namespace CarDealer.Dtos.Export
{
    using System;
    using System.Xml.Serialization;

    [XmlType("car")]
    public class ExportDistanceCarsDto
    {
        public ExportDistanceCarsDto()
        {
        }

        [XmlElement("Make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public long TravelledDistance { get; set; }
    }
}
