namespace OnlineShop.Models.Products.Peripherals
{
    using System;

    public abstract class Peripheral : Product, IPeripheral
    {
        protected Peripheral(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.ConnectionType = connectionType;
        }

        public string ConnectionType { get;}

        //public override string ToString()
        //{
        //    return $"Overall Performance: {this.OverallPerformance:F2}. Price: {this.Price:F2} - {this.GetType().Name}: {this.Manufacturer} {this.Model} (Id: {this.Id}) Connection Type: {this.ConnectionType}";
        //}

        public override string ToString()
        {
            return base.ToString() + $" Connection Type: {this.ConnectionType}";
        }
    }
}
