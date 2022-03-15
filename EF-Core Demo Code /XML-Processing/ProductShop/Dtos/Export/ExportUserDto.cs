namespace ProductShop.Dtos.Export
{
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    
    using ProductShop.Models;

    [XmlType("User")]
    public class ExportUserDto
    {
        public ExportUserDto()
        {
        }

        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlArray("soldProducts")]
        public ExportSoldProductDto[] SoldProducts { get; set; }

        //[XmlArray("soldProducts")]
        //public List<ExportSoldProductDto> SoldProducts { get; set; } = new List<ExportSoldProductDto>();
    }
}
