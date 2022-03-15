namespace ProductShop.Dtos.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Product")]
    public class ImportProductDto
    {
        public ImportProductDto()
        {
        }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("sellerId")]
        public int SellerId { get; set; }

        [XmlElement("buyerId")]
        public int? BuyerId { get; set; }
        // As in Product Class, BuyerId must be nullable (a buyer can be missing)
        // otherwise will fail to insert into Database
    }
}
