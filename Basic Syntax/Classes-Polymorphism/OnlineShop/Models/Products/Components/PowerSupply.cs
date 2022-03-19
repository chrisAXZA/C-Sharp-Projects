namespace OnlineShop.Models.Products.Components
{
    using System;

    public class PowerSupply : Component
    {
        private const double PERFORMANCE_MULTIPLIER = 1.05;

        public PowerSupply(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance * PERFORMANCE_MULTIPLIER, generation)
        {
        }
    }
}
