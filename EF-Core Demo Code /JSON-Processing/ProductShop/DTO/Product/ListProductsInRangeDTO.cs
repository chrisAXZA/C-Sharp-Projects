namespace ProductShop.DTO.Product
{
    using System;
    
    using Newtonsoft.Json;

    public class ListProductsInRangeDTO
    {
        public ListProductsInRangeDTO()
        {
        }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("seller")]
        public string SellerName { get; set; }
    }
}
