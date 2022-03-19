namespace OnlineShop.Models.Products.Components
{
    using System;

    using OnlineShop.Models.Products.Components;

    public abstract class Component : Product, IComponent
    {
        //int id, string manufacturer, string model, decimal price, double overallPerformance, int generation
        protected Component(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.Generation = generation;
        }

        public int Generation { get; private set; }

        //public override string ToString()
        //{
        //    return $"Overall Performance: {this.OverallPerformance:F2}. Price: {this.Price:F2} - {this.GetType().Name}: {this.Manufacturer} {this.Model} (Id: {this.Id}) Generation: {this.Generation}";
        //}

        public override string ToString()
        {
            return base.ToString() + $" Generation: {this.Generation}";
        }
    }
}
