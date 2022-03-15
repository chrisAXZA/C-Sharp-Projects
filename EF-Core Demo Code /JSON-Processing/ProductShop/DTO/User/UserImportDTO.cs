namespace ProductShop.DTO.User
{
    using System;
    
    using Newtonsoft.Json;

    public class UserImportDTO
    {
        public UserImportDTO()
        {
        }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }
    }
}
