﻿namespace OnlineShop.Models.Products.Components
{
    using System;

    public class CentralProcessingUnit : Component
    {
        private const double PERFORMANCE_MULTIPLIER = 1.25;
       
        public CentralProcessingUnit(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance * PERFORMANCE_MULTIPLIER, generation)
        {
        }
    }
}
