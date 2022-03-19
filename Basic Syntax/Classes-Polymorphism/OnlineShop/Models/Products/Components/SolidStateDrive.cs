namespace OnlineShop.Models.Products.Components
{
    using System;

    public class SolidStateDrive : Component
    {
        private const double PERFORMANCE_MULTIPLIER = 1.20;

        public SolidStateDrive(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance * PERFORMANCE_MULTIPLIER, generation)
        {
        }
    }
}
