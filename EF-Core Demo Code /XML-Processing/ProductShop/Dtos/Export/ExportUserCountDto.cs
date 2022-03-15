namespace ProductShop.Dtos.Export
{
    using System;
    using System.Xml.Serialization;

    // XmlType not required, set with RootElement given to XmlSerializer
    public class ExportUserCountDto
    {
        public ExportUserCountDto()
        {
        }

        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("users")]
        public ExportNestedUserDto[] Users { get; set; }
    }

    [XmlType("User")]
    public class ExportNestedUserDto
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }

        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("age")]
        public int? Age { get; set; }

        [XmlElement("SoldProducts")]
        public ExportNestedProductsDto SoldProducts { get; set; }

    }

    //[XmlType("SoldProducts")]
    public class ExportNestedProductsDto
    {
        [XmlElement("count")]
        public int Count { get; set; }

        [XmlArray("products")]
        public ExportNestedProductDetailDto[] Products { get; set; }
    }

    [XmlType("Product")]
    public class ExportNestedProductDetailDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }
    }

    //[XmlRoot("Users")]
    //public class UserProductsFinalModel
    //{
    //    [XmlElement("count")]
    //    public int Count { get; set; }

    //    [XmlArray("users")]
    //    public UserExportModel[] Users { get; set; }
    //}
}
