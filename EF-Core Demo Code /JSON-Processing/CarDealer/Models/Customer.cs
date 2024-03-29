﻿namespace CarDealer.Models
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsYoungDriver { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}