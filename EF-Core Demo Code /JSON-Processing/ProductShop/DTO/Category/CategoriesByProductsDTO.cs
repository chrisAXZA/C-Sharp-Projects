namespace ProductShop.DTO.Category
{
    using System;

    using Newtonsoft.Json;

    public class CategoriesByProductsDTO
    {
        public CategoriesByProductsDTO()
        {
        }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("productsCount")]
        public int ProductsCount { get; set; }

        [JsonProperty("averagePrice")]
        public string AveragePrice { get; set; }

        [JsonProperty("totalRevenue")]
        public string TotalRevenue { get; set; }
    }
}
