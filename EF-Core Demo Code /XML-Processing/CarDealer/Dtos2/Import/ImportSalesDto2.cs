namespace CarDealer.Dtos2.Import
{
    using System;
    using System.Xml.Serialization;

    [XmlType("Sale")]
    public class ImportSalesDto2
    {
        public ImportSalesDto2()
        {
        }

        [XmlElement("carId")]
        public int CarId { get; set; }

        [XmlElement("customerId")]
        public int CustomerId { get; set; }

        [XmlElement("discount")]
        public decimal Discount { get; set; }
    }
}
