﻿namespace ProductShop.Dtos.Export
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Product")]
    public class ExportSoldProductDto
    {
        public ExportSoldProductDto()
        {
        }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }
}
