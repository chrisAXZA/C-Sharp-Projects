namespace OnlineShop.Models.Products.Components
{
    using System;

    public class Motherboard : Component
    {
        private const double PERFORMANCE_MULTIPLIER = 1.25;

        public Motherboard(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance * PERFORMANCE_MULTIPLIER, generation)
        {
        }
    }
}
