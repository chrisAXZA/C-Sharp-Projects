﻿namespace CarDealer.Models
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    public class PartCar
    {
        public int PartId { get; set; }
        public Part Part { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
