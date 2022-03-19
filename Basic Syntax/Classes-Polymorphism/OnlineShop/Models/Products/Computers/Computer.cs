namespace OnlineShop.Models.Products.Computers
{
    using System;
    using System.Text;
    using System.Linq;
    using System.Collections.Generic;

    using OnlineShop.Common.Enums;
    using OnlineShop.Common.Constants;
    using OnlineShop.Models.Products.Components;
    using OnlineShop.Models.Products.Peripherals;

    public abstract class Computer : Product, IComputer
    {
        private readonly ICollection<IComponent> components;
        private readonly ICollection<IPeripheral> peripherals;

        private decimal price;
        private double overallPerformance;

        protected Computer(int id, string manufacturer, string model, decimal price, double overallPerformance)
            : base(id, manufacturer, model, price, overallPerformance)
        {
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();

            this.price = price;
            this.overallPerformance = overallPerformance;
        }

        public IReadOnlyCollection<IComponent> Components => (IReadOnlyCollection<IComponent>)this.components;

        public IReadOnlyCollection<IPeripheral> Peripherals => (IReadOnlyCollection<IPeripheral>)this.peripherals;

        public override decimal Price => CalculatePrice();

        public override double OverallPerformance => CalculatePerformance();

        public void AddComponent(IComponent component)
        {
            //if (this.components.Contains(component))
            //{
            //    throw new ArgumentException($"Component {component.GetType().Name} already exists in {this.GetType().Name} with Id {this.Id}.");
            //}

            if (this.components.Any(c => c.GetType().Name == component.GetType().Name))
            {
                throw new ArgumentException($"Component {component.GetType().Name} already exists in {this.GetType().Name} with Id {this.Id}.");
            }

            bool result = Enum.TryParse(component.GetType().Name, out ComponentType currentComponent);

            if (!result)
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            this.components.Add(component);
        }

        public void AddPeripheral(IPeripheral peripheral)
        {
            //if (this.peripherals.Contains(peripheral))
            //{
            //    throw new ArgumentException($"Peripheral {peripheral.GetType().Name} already exists in {this.GetType().Name} with Id {this.Id}.");
            //}

            if (this.peripherals.Any(p => p.GetType().Name == peripheral.GetType().Name))
            {
                throw new ArgumentException($"Peripheral {peripheral.GetType().Name} already exists in {this.GetType().Name} with Id {this.Id}.");
            }

            bool result = Enum.TryParse(peripheral.GetType().Name, out PeripheralType currentPeripheral);

            if (!result)
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            this.peripherals.Add(peripheral);
        }

        public IComponent RemoveComponent(string componentType)
        {
            IComponent componentTarget = this.components.FirstOrDefault(com => com.GetType().Name == componentType);

            if (this.components.Count == 0 || componentTarget == null)
            {
                throw new ArgumentException($"Component {componentType} does not exist in {this.GetType().Name} with Id {this.Id}.");
            }

            this.components.Remove(componentTarget);

            return componentTarget;
        }

        public IPeripheral RemovePeripheral(string peripheralType)
        {
            IPeripheral peripheralTarget = this.peripherals.FirstOrDefault(per => per.GetType().Name == peripheralType);

            if (this.peripherals.Count == 0 || peripheralTarget == null)
            {
                throw new ArgumentException($"Peripheral {peripheralType} does not exist in {this.GetType().Name} with Id {this.Id}.");
            }

            this.peripherals.Remove(peripheralTarget);

            return peripheralTarget;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Overall Performance: {this.OverallPerformance:F2}. Price: {this.Price:F2} - {this.GetType().Name}: {this.Manufacturer} {this.Model} (Id: {this.Id})");
            sb.AppendLine($" Components ({this.components.Count}):");

            foreach (var component in this.components)
            {
                sb.AppendLine($"  {component.ToString().Trim()}");
            }

            double peripheralPerformanceAverage = this.peripherals.Sum(per => per.OverallPerformance) / this.peripherals.Count;

            if (this.peripherals.Count == 0)
            {
                peripheralPerformanceAverage = 0;
            }

            sb.AppendLine($" Peripherals ({this.peripherals.Count}); Average Overall Performance ({peripheralPerformanceAverage:F2}):");

            foreach (var peripheral in this.peripherals)
            {
                sb.AppendLine($"  {peripheral.ToString().Trim()}");
            }

            return sb.ToString().Trim();
        }

        private decimal CalculatePrice()
        {
            decimal totalPrice = this.price + this.components.Sum(com => com.Price) + this.peripherals.Sum(per => per.Price);

            if (this.price <= 0)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPrice);
            }

            if (totalPrice <= 0)
            {
                throw new ArgumentException(ExceptionMessages.InvalidPrice);
            }

            return totalPrice;
        }

        private double CalculatePerformance()
        {
            double currentPerfomance = this.overallPerformance;

            if (currentPerfomance <= 0)
            {
                throw new ArgumentException(ExceptionMessages.InvalidOverallPerformance);
            }

            if (this.components.Count == 0)
            {
                return currentPerfomance;
            }

            double componentsPerformance = this.components.Sum(com => com.OverallPerformance) / this.components.Count;

            return currentPerfomance + componentsPerformance;
        }
    }
}
