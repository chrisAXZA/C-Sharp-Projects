namespace Tests
{
    using System;
    
    using NUnit.Framework;
    
    using CarManager;

    [TestFixture]
    public class CarTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;
            double expectedFuelAmount = 0;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.AreEqual(expectedMake, car.Make);
            Assert.AreEqual(expectedModel, car.Model);
            Assert.AreEqual(expectedFuelConsumption, car.FuelConsumption);
            Assert.AreEqual(expectedFuelCapacity, car.FuelCapacity);
            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);
        }

        [Test]
        public void MakeValidatorWithNullMakeShouldThrowException()
        {
            string expectedMake = null;
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            });
        }

        [Test]
        public void MakeValidatorWithEmptyMakeShouldThrowException()
        {
            string expectedMake = string.Empty;
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            });
        }

        [Test]
        public void ModelValidatorWithNullModelShouldThrowException()
        {
            string expectedMake = "VW";
            string expectedModel = null;
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            });
        }

        [Test]
        public void ModelValidatorWithEmptyModelShouldThrowException()
        {
            string expectedMake = "VW";
            string expectedModel = string.Empty;
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            });
        }

        [Test]
        public void FuelConsumptionValidatorWithNegativeValueShouldThrowException()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = -2;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            });
        }

        [Test]
        public void FuelConsumptionValidatorWithZeroValueShouldThrowException()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 0;
            double expectedFuelCapacity = 100;

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            });
        }

        [Test]
        public void TestFuelCapacityValidatorWithNegativeValue()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = -100;

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            });
        }

        [Test]
        public void TestFuelCapacityValidatorWithZeroValue()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 0;

            Assert.Throws<ArgumentException>(() =>
            {
                Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            });
        }

        [Test]
        public void TestRefuelMethodWithZeroValue()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.Throws<ArgumentException>(() =>
            {
                car.Refuel(0);
            });
        }

        [Test]
        public void TestRefuelMethodWithNegativeValue()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.Throws<ArgumentException>(() =>
            {
                car.Refuel(-20);
            });
        }

        [Test]
        public void TestRefuelMethodWorksCorrectly()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            double expectedFuelAmount = 20;
            car.Refuel(expectedFuelAmount);

            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);
        }

        [Test]
        public void TestRefuelMethodWorksCorrectlyWithFuelBiggerThanFuelCapacity()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            double fuelAmount = 200;
            car.Refuel(fuelAmount);

            Assert.AreEqual(expectedFuelCapacity, car.FuelAmount);
        }

        [Test]
        public void TestRefuelMethodWorksCorrectlyWhenFuelCapacityEqualsFuelAmount()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            double fuelAmount = 100;
            car.Refuel(fuelAmount);

            Assert.AreEqual(expectedFuelCapacity, car.FuelAmount);
        }

        //TODO: How to set value FuelAmount to negative
        [Test]
        public void TestDriveMethodThrowsExceptionWhenFuelAmountIsZero()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);

            Assert.Throws<InvalidOperationException>(() =>
            {
                car.Drive(100);
            });
        }

        [Test]
        public void TestDriveMethodWorksCorrectly()
        {
            string expectedMake = "VW";
            string expectedModel = "Golf";
            double expectedFuelConsumption = 2;
            double expectedFuelCapacity = 100;

            Car car = new Car(expectedMake, expectedModel, expectedFuelConsumption, expectedFuelCapacity);
            double fuelAmount = 4;
            car.Refuel(fuelAmount);

            double distance = 100;
            double fuelNeeded = (distance / 100) * expectedFuelConsumption;
            double expectedFuelAmount = car.FuelAmount - fuelNeeded;

            car.Drive(distance);

            Assert.AreEqual(expectedFuelAmount, car.FuelAmount);
        }
    }
}