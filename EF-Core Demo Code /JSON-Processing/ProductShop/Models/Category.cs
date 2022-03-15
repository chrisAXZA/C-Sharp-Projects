namespace ProductShop.Models
{
    using System.Collections.Generic;
    
    using Newtonsoft.Json;

    public class Category
    {
        public Category()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }

        public int Id { get; set; }

        [JsonProperty("name")] // prop "Name" will be serialied in JSON file with "name"
        public string Name { get; set; }

        public ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}
