﻿namespace CarDealer.DTO
{
    using System;
    
    using Newtonsoft.Json;

    public class ImportCarDTO
    {
        public ImportCarDTO()
        {
        }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("travelledDistance")]
        public long TravelledDistance { get; set; }

        [JsonProperty("partsId")]
        public int[] PartsId { get; set; }
    }
}
