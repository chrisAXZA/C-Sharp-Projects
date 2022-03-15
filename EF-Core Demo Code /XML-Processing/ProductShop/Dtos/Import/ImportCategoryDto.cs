﻿namespace ProductShop.Dtos.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Category")]
    public class ImportCategoryDto
    {
        public ImportCategoryDto()
        {
        }

        [XmlElement("name")]
        public string Name { get; set; }
    }
}
