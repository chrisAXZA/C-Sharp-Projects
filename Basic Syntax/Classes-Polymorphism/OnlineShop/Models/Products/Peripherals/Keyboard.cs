namespace OnlineShop.Models.Products.Peripherals
{
    using System;

    public class Keyboard : Peripheral
    {
        public Keyboard(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
            : base(id, manufacturer, model, price, overallPerformance, connectionType)
        {
        }
    }
}
