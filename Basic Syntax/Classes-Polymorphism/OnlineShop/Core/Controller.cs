namespace OnlineShop.Core
{
    using System;
    using System.Linq;
    //using System.ComponentModel;
    using System.Collections.Generic;

    using OnlineShop.Common.Enums;
    using OnlineShop.Models.Products;
    using OnlineShop.Common.Constants;
    using OnlineShop.Models.Products.Computers;
    using OnlineShop.Models.Products.Components;
    using OnlineShop.Models.Products.Peripherals;

    public class Controller : IController
    {
        private readonly ICollection<IComputer> computers;
        private readonly ICollection<IComponent> components;
        private readonly ICollection<IPeripheral> peripherals;

        public Controller()
        {
            this.computers = new List<IComputer>();
            this.components = new List<IComponent>();
            this.peripherals = new List<IPeripheral>();
        }

        public string AddComponent(int computerId, int id, string componentType, string manufacturer, string model, decimal price, double overallPerformance, int generation)
        {
            // IComputer computerTarget = this.computers.First(com => com.Id == computerId);

            if (!this.computers.Any(com => com.Id == computerId))
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            if (this.components.Any(com => com.Id == id))
            {
                throw new ArgumentException("Component with this id already exists.");
            }

            IComponent newComponent = null;

            if (componentType == nameof(CentralProcessingUnit))
            {
                newComponent = new CentralProcessingUnit(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(Motherboard))
            {
                newComponent = new Motherboard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(PowerSupply))
            {
                newComponent = new PowerSupply(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(RandomAccessMemory))
            {
                newComponent = new RandomAccessMemory(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(SolidStateDrive))
            {
                newComponent = new SolidStateDrive(id, manufacturer, model, price, overallPerformance, generation);
            }
            else if (componentType == nameof(VideoCard))
            {
                newComponent = new VideoCard(id, manufacturer, model, price, overallPerformance, generation);
            }
            else
            {
                throw new ArgumentException("Component type is invalid.");
            }

            IComputer computerTarget = this.computers.First(com => com.Id == computerId);

            computerTarget.AddComponent(newComponent);

            this.components.Add(newComponent);

            return $"Component {componentType} with id {newComponent.Id} added successfully in computer with id {computerId}.";
        }

        // NO COMPUTER CHECK
        public string AddComputer(string computerType, int id, string manufacturer, string model, decimal price)
        {
            IComputer computer = null;

            if (this.computers.Any(com => com.Id == id))
            {
                throw new ArgumentException("Computer with this id already exists.");
            }

            if (computerType == nameof(DesktopComputer))
            {
                computer = new DesktopComputer(id, manufacturer, model, price);
            }
            else if (computerType == nameof(Laptop))
            {
                computer = new Laptop(id, manufacturer, model, price);
            }
            else
            {
                throw new ArgumentException("Computer type is invalid.");
            }

            this.computers.Add(computer);

            return $"Computer with id {id} added successfully.";
        }

        public string AddPeripheral(int computerId, int id, string peripheralType, string manufacturer, string model, decimal price, double overallPerformance, string connectionType)
        {
            if (!this.computers.Any(com => com.Id == computerId))
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            if (this.peripherals.Any(per => per.Id == id))
            {
                throw new ArgumentException("Peripheral with this id already exists.");
            }

            IPeripheral peripheral = null;

            if (peripheralType == nameof(Headset))
            {
                peripheral = new Headset(id, manufacturer, model, price, overallPerformance, connectionType);
            }
            else if (peripheralType == nameof(Keyboard))
            {
                peripheral = new Keyboard(id, manufacturer, model, price, overallPerformance, connectionType);

            }
            else if (peripheralType == nameof(Monitor))
            {
                peripheral = new Monitor(id, manufacturer, model, price, overallPerformance, connectionType);

            }
            else if (peripheralType == nameof(Mouse))
            {
                peripheral = new Mouse(id, manufacturer, model, price, overallPerformance, connectionType);

            }
            else
            {
                throw new ArgumentException("Peripheral type is invalid.");
            }

            IComputer computerTarget = this.computers.First(com => com.Id == computerId);

            computerTarget.AddPeripheral(peripheral);

            this.peripherals.Add(peripheral);

            return $"Peripheral {peripheralType} with id {peripheral.Id} added successfully in computer with id {computerId}.";
        }

        public string BuyBest(decimal budget)
        {
            if (this.computers.Count == 0)
            {
                throw new ArgumentException($"Can't buy a computer with a budget of ${budget}.");
            }

            var performanceComputers = this.computers
                .OrderByDescending(com => com.OverallPerformance);

            bool successfullBuy = false;
            IComputer computerTarget = null;
            //IComputer computerTarget2 = performanceComputers.FirstOrDefault(com => com.Price <= budget);

            foreach (var computer in performanceComputers)
            {
                if (computer.Price <= budget)
                {
                    computerTarget = computer;
                    successfullBuy = true;
                    break;
                }
            }

            // if(computerTarget2 == null)
            if (successfullBuy == false)
            {
                throw new ArgumentException($"Can't buy a computer with a budget of ${budget}.");
            }

            this.computers.Remove(computerTarget);

            return computerTarget.ToString().Trim();
        }

        public string BuyComputer(int id)
        {
            if (!this.computers.Any(com => com.Id == id))
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            IComputer computerTarget = this.computers.First(com => com.Id == id);

            this.computers.Remove(computerTarget);

            return computerTarget.ToString().Trim();
        }

        public string GetComputerData(int id)
        {
            if (!this.computers.Any(com => com.Id == id))
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            IComputer computerTarget = this.computers.FirstOrDefault(com => com.Id == id);

            return computerTarget.ToString().Trim();
        }

        public string RemoveComponent(string componentType, int computerId)
        {
            if (!this.computers.Any(com => com.Id == computerId))
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            bool result = Enum.TryParse(componentType, out ComponentType currentComponent);

            if (!result)
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            IComputer computerTarget = this.computers.First(com => com.Id == computerId);

            IComponent removedComponent = computerTarget.RemoveComponent(componentType);

            this.components.Remove(removedComponent);

            return $"Successfully removed {componentType} with id {removedComponent.Id}.";
        }

        public string RemovePeripheral(string peripheralType, int computerId)
        {
            if (!this.computers.Any(com => com.Id == computerId))
            {
                throw new ArgumentException("Computer with this id does not exist.");
            }

            bool result = Enum.TryParse(peripheralType, out PeripheralType currentPeripheral);

            if (!result)
            {
                throw new ArgumentException(ExceptionMessages.InvalidComponentType);
            }

            IComputer computerTarget = this.computers.First(com => com.Id == computerId);

            IPeripheral removedPeripheral = computerTarget.RemovePeripheral(peripheralType);

            this.peripherals.Remove(removedPeripheral);

            return $"Successfully removed {peripheralType} with id {removedPeripheral.Id}.";
        }
    }
}
