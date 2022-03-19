namespace OnlineShop.Models.Products.Peripherals
{
    using System;

    public class Monitor : Peripheral
    {
        public Monitor(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
            : base(id, manufacturer, model, price, overallPerformance, connectionType)
        {
        }
    }
}
