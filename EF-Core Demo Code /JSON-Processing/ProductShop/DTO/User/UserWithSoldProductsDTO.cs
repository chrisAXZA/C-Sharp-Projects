namespace ProductShop.DTO.User
{
    using System;
    using System.Collections.Generic;
    
    using Newtonsoft.Json;

    public class UserWithSoldProductsDTO
    {
        public UserWithSoldProductsDTO()
        {
        }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("soldProducts")]
        public UserSoldProductDTO[] SoldProducts { get; set; }
        //public ICollection<UserSoldProductDTO> SoldProducts { get; set; }
    }
}
