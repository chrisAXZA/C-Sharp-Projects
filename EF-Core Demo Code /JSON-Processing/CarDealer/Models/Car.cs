namespace CarDealer.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public class Car
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelledDistance { get; set; }

        public ICollection<Sale> Sales { get; set; }

        public ICollection<PartCar> PartCars { get; set; } = new List<PartCar>();
    }
}