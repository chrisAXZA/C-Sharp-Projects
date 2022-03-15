namespace CarManager.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class CarTests2
    {
        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;
            //double expectedFuelAmount = 0;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.AreEqual(expectedMake, car.Make);
            Assert.AreEqual(expectedModel, car.Model);
            Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
            //Assert.AreEqual(expectedFuelAmount, car.FuelAmount);
        }

        [Test]
        public void ModelShouldThrowArgumentExceptionWhenNameIsEmpty()
        {
            string expectedMake = "VW";
            string expectedModel = null;
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(()
                => new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity));
        }

        [Test]
        public void MakeShouldThrowArgumentExceptionWhenNameIsEmpty()
        {
            string expectedMake = null;
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(()
                => new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity));
        }

        [Test]
        public void FuelConsumptionShouldThrowArgumentExceptionWhenValueIsNegative()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = -2;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(()
                => new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity));
        }

        [Test]
        public void FuelConsumptionShouldThrowArgumentExceptionWhenValueIsZero()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 0;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(()
                => new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity));
        }

        [Test]
        public void FuelCapacityShouldThrowArgumentExceptionWhenValueIsZero()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 0;

            Assert.Throws<ArgumentException>(()
                => new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity));
        }

        [Test]
        public void FuelCapacityShouldThrowArgumentExceptionWhenValueIsNegative()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = -100;

            Assert.Throws<ArgumentException>(()
                => new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity));
        }

        //[TestCase(null, "Golf", 2, 100)]
        //[TestCase("VW", null, 2, 100)]
        //[TestCase("VW", "Golf", -2, 100)]
        //[TestCase("VW", "Golf", 0, 100)]
        //[TestCase(null, "Golf", 2, -100)]
        //[TestCase(null, "Golf", 2, 0)]
        //public void AllPropertiesShouldThrowExceptionWhenGivenInvalidValues
        //    (string make, string model, double fuelConsumption, double fuelCapacity)
        //{
        //    Assert.Throws<ArgumentException>(()
        //    => new Car(make, model, fuelConsumption, fuelCapacity));
        //}

        [Test]
        public void TestRefuelMethodWorksCorrectly()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            car.Refuel(10);

            double expectedFuelAmount = 10;
            double actualFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [Test]
        public void TestRefuelMethodWorksCorrectlyWhenFuelCapacityIsGivenBiggerFuelAmount()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 50;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            car.Refuel(100);

            double expectedFuelAmount = 50;
            double actualFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAmount);
        }

        [TestCase(-10)]
        [TestCase(0)]
        public void RefuelMethodShouldThrowArguementExceptionWhenFuelAmountIsNegativeOrZero(double fuelAmount)
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;
            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.Throws<ArgumentException>(() => car.Refuel(fuelAmount));
        }

        [Test]
        public void TestDriveMethodWorksCorrectly()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;
            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            car.Refuel(20);
            car.Drive(20);

            double expectedfuelAmount = 19.6;
            double actualFuelAmount = car.FuelAmount;

            Assert.AreEqual(expectedfuelAmount, actualFuelAmount);
        }

        [Test]
        public void TestDriveIfThrowsInvalidOperationExceptionWhenFuelNeededExceedsFuelAmount()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.Throws<InvalidOperationException>(()
                => car.Drive(20));
        }
    }
}
