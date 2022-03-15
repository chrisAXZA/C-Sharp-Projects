namespace Chainblock.Tests
{
    using System;

    using NUnit.Framework;

    using Chainblock.Common;
    using Chainblock.Models;
    using Chainblock.Contracts;

    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            // Tests class ctor and props at the same time
            Assert.AreEqual(id, transaction.Id);
            Assert.AreEqual(ts, transaction.Status); // validates enum int : 0 == 0, 1 == 1
            Assert.AreEqual(from, transaction.From);
            Assert.AreEqual(to, transaction.To);
            Assert.AreEqual(amount, transaction.Amount);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-10)]
        public void TestConstructorWithInvalidId(int id)
        {
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            //Assert.Throws<ArgumentException>(()
            //    => new Transaction(id, ts, from, to, amount));
            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.EqualTo
            (ExceptionMessages.InvalidIdMessage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("         ")]
        public void TestConstructorWithInvalidSenderUsername(string from)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string to = "Gosho";
            double amount = 15;

            //Assert.Throws<ArgumentException>(()
            //    => new Transaction(id, ts, from, to, amount));
            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.EqualTo
            (ExceptionMessages.InvalidSenderUsernameMessage));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("         ")]
        public void TestConstructorWithInvalidReceiverUsername(string to)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            double amount = 15;

            //Assert.Throws<ArgumentException>(()
            //    => new Transaction(id, ts, from, to, amount));
            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.EqualTo
            (ExceptionMessages.InvalidReceiverUsernameMessage));
        }

        [Test]
        [TestCase(0.0)]
        [TestCase(-10.0)]
        [TestCase(-0.00000001)]
        public void TestConstructorWithInvalidAmount(double amount)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            //Assert.Throws<ArgumentException>(()
            //    => new Transaction(id, ts, from, to, amount));
            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.EqualTo
            (ExceptionMessages.InvalidTransactionAmountMessage));
        }
    }
}
