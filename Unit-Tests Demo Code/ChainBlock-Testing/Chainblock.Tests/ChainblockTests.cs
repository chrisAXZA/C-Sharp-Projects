namespace Chainblock.Tests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using NUnit.Framework;

    using Chainblock.Common;
    using Chainblock.Models;
    using Chainblock.Contracts;

    [TestFixture]
    public class ChainblockTests
    {
        private IChainblock chainblock;
        private ITransaction testTransaction;

        [SetUp]
        public void Setup()
        {
            this.chainblock = new Core.Chainblock();
            this.testTransaction = new Transaction
                (4, TransactionStatus.Unauthorized, "Pesho", "Gosho", 10);
        }

        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            // Will test Count-prop and Chainblock-ctor
            int expectedCount = 0;
            // IChainblock chainblock = new Core.Chainblock();

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void TestIfAddIncreasesCountWhenSuccessfull()
        {
            // Arrange
            int expectedCount = 1;
            ITransaction transaction = new Transaction
                (1, TransactionStatus.Successfull, "Pesho", "Gosho", 10.0);
            // Act
            this.chainblock.Add(transaction);
            // Assert
            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void AddingSameTransactionWithDifferentIdShouldBeSuccessfull()
        {
            // Arrange
            int expectedCount = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(1, ts, from, to, amount);
            ITransaction transactionCopy = new Transaction(2, ts, from, to, amount);

            // Act
            this.chainblock.Add(transaction);
            this.chainblock.Add(transactionCopy);

            // Assert
            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void AddingAlreadyExistingTransactionShouldThrowArgumentException()
        {
            // Arrange
            ITransaction transaction = new Transaction
                (1, TransactionStatus.Failed, "Pesho", "Gosho", 10.0);

            // Act
            this.chainblock.Add(transaction);

            // Assert
            Assert.That(() =>
            {
                this.chainblock.Add(transaction);
            }, Throws.InvalidOperationException
            .With.Message.EqualTo(ExceptionMessages.InvalidAddExistingTransactionMessage));
        }

        [Test]
        public void TransactionContainsShouldReturnTrueForExistingTransactionInRepository()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);

            Assert.IsTrue(this.chainblock.Contains(transaction));
        }

        [Test]
        public void TransactionContainsShouldReturnFalseForNonExistingTransactionInRepository()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            // this.chainblock.Add(transaction);

            Assert.IsFalse(this.chainblock.Contains(transaction));
        }

        [Test]
        public void IdContainsShouldReturnTrueForExistingTransactionInRepository()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);

            Assert.IsTrue(this.chainblock.Contains(id));
        }

        [Test]
        public void IdContainsShouldReturnFalseForExistingTransactionInRepository()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            Assert.IsFalse(this.chainblock.Contains(id));
        }

        [Test]
        public void ChangeTransactionStatusShouldChangeStatusOfExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            TransactionStatus newStatus = TransactionStatus.Successfull;

            this.chainblock.Add(transaction);

            this.chainblock.ChangeTransactionStatus(id, TransactionStatus.Successfull);

            //Assert.That(transaction.Status == TransactionStatus.Successfull);

            Assert.AreEqual(newStatus, transaction.Status);
        }

        [Test]
        public void ChangingStatusOfNonExistingTransactionShouldThrowArgumentException()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            TransactionStatus newStatus = TransactionStatus.Successfull;

            this.chainblock.Add(transaction);

            int fakeId = 99;

            //Assert.That(transaction.Status == TransactionStatus.Successfull);

            Assert.That(() =>
            {
                this.chainblock.ChangeTransactionStatus(fakeId, newStatus);
            }, Throws.ArgumentException.With.Message.EqualTo
            (ExceptionMessages.ChangeStatusOfNonExistingTransactionMessage));
        }

        [Test]
        public void GetByIdShouldReturnExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(this.testTransaction);

            ITransaction transactionTarget = this.chainblock.GetById(id);

            Assert.AreEqual(transaction, transactionTarget);

            // Or
            // Assert.AreEqual(transaction.Id, transactionTarget.Id);

            // OR
            // Assert.That(transactionTarget, Is.EqualTo(transaction));
        }

        [Test]
        public void GetByIdShouldShouldThrowInvalidOperationExceptionWithNonExistingTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            int fakeId = 99;

            this.chainblock.Add(transaction);
            this.chainblock.Add(this.testTransaction);

            Assert.That(() =>
            {
                ITransaction transactionTarget = this.chainblock.GetById(fakeId);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.GetIdOfNonExistingTransactionMessage));
        }

        [Test]
        public void RemoveTransactionByIdOperationShouldDecreaseCountIfSuccessfull()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            int expectedCount = 1;

            this.chainblock.Add(transaction);
            this.chainblock.Add(this.testTransaction);

            this.chainblock.RemoveTransactionById(id);

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void RemoveTransactionByIdOperationShouldRemoveTransactionFromRepository()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            this.chainblock.Add(transaction);
            this.chainblock.Add(this.testTransaction);

            this.chainblock.RemoveTransactionById(id);

            Assert.That(() =>
            {
                this.chainblock.GetById(id);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.GetIdOfNonExistingTransactionMessage));
        }

        [Test]
        public void RemoveTransactionByIdShouldThrowInvalidOperationExceptionForNonExistantTransaction()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Receiver";
            double amount = 15;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            int fakeId = 99;

            this.chainblock.Add(transaction);
            this.chainblock.Add(this.testTransaction);

            Assert.That(() =>
            {
                this.chainblock.RemoveTransactionById(fakeId);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.RemoveNonExistingTransactionMessage));
        }

        [Test]
        public void GetByTransactionStatusShouldReturnOrderedCollectionOfTransactionsIfSuccessfull()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int currentId = i + 1;
                TransactionStatus status = (TransactionStatus)i;
                string currentFrom = "Pesho" + i;
                string currentTo = "Gosho" + i;
                double currentAmount = 10;
                ITransaction currentTransaction = new Transaction
                    (currentId, status, currentFrom, currentTo, currentAmount);

                if (status == TransactionStatus.Successfull)
                {
                    expectedTransactions.Add(currentTransaction);
                }

                this.chainblock.Add(currentTransaction);
            }

            ITransaction transactionSuccess = new Transaction(5, TransactionStatus.Successfull, "Pesho4", "Gosho4", 15);
            expectedTransactions.Add(transactionSuccess);
            this.chainblock.Add(transactionSuccess);

            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetByTransactionStatus(TransactionStatus.Successfull);

            expectedTransactions = expectedTransactions
                .OrderByDescending(t => t.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);

            //int id = 1;
            //TransactionStatus ts = TransactionStatus.Successfull;
            //string from = "Sender";
            //string to = "Receiver";
            //double amount = 15;
            //double amount2 = 30;

            //int id2 = 44;

            //ITransaction transaction = new Transaction(id, ts, from, to, amount);
            //ITransaction transaction2 = new Transaction(id2, ts, from, to, amount2);

            //this.chainblock.Add(transaction);
            //this.chainblock.Add(transaction2);
            //this.chainblock.Add(this.testTransaction);

            //ITransaction[] transactions = this.chainblock.GetByTransactionStatus(TransactionStatus.Successfull).ToArray();

            //int expectedCount = 2;

            //Assert.AreEqual(expectedCount, transactions.Length);
            //Assert.That(transactions[0].Amount > transactions[1].Amount);
        }

        [Test]
        public void GetByTransactionStatusShouldThrowArgumentExceptionIfForNonExistantTransaction()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Failed;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 * (i + 1);

                ITransaction currentT = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentT);
            }

            Assert.That(() =>
            {
                this.chainblock.GetByTransactionStatus(TransactionStatus.Successfull);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.NonExistingTransactionWithGivenStatusMessage));

            //int id = 1;
            //TransactionStatus ts = TransactionStatus.Successfull;
            //string from = "Sender";
            //string to = "Receiver";
            //double amount = 15;

            //int id2 = 44;

            //ITransaction transaction = new Transaction(id, ts, from, to, amount);
            //ITransaction transaction2 = new Transaction(id2, ts, from, to, amount);

            //this.chainblock.Add(transaction);
            //this.chainblock.Add(transaction2);

            //Assert.That(() =>
            //{
            //    ITransaction[] transactions = this.chainblock.GetByTransactionStatus(TransactionStatus.Failed).ToArray();

            //}, Throws.InvalidOperationException.With.Message.EqualTo
            //(ExceptionMessages.NonExistingTransactionWithGivenStatusMessage));
        }

        [Test]
        public void GetAllSendersWithTransactionStatusShouldReturnOrderedCollectionOfSendersIfSuccessfull()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int currentId = i + 1;
                TransactionStatus status = (TransactionStatus)i;
                string currentFrom = "Pesho" + i;
                string currentTo = "Gosho" + i;
                double currentAmount = 10;
                ITransaction currentTransaction = new Transaction
                    (currentId, status, currentFrom, currentTo, currentAmount);

                if (status == TransactionStatus.Successfull)
                {
                    expectedTransactions.Add(currentTransaction);
                }

                this.chainblock.Add(currentTransaction);
            }

            ITransaction transactionSuccess = new Transaction(5, TransactionStatus.Successfull, "Pesho4", "Gosho4", 15);
            expectedTransactions.Add(transactionSuccess);
            this.chainblock.Add(transactionSuccess);

            IEnumerable<string> actualTransactionOutput = this.chainblock
                .GetAllSendersWithTransactionStatus(TransactionStatus.Successfull);

            IEnumerable<string> expectedTransactionsOutput = expectedTransactions
                .OrderByDescending(t => t.Amount)
                .Select(t => t.From);

            CollectionAssert.AreEqual(expectedTransactionsOutput, actualTransactionOutput);

            //int id = 1;
            //TransactionStatus status = TransactionStatus.Successfull;
            //string from = "Gosho";
            //string to = "Pesho";
            //double amount = 10;

            //ITransaction transaction1 = new Transaction(id, status, from, to, amount);
            //ITransaction transaction2 = new Transaction(id + 1, status, from, to, amount * 2);
            //ITransaction transaction3 = new Transaction(id + 2, status, to, from, amount * 3);

            //int expectedCount = 3;
            //string expectedName = "Pesho";

            //this.chainblock.Add(transaction1);
            //this.chainblock.Add(transaction2);
            //this.chainblock.Add(transaction3);

            //IEnumerable<string> senders = this.chainblock
            //    .GetAllSendersWithTransactionStatus(TransactionStatus.Successfull);

            //Assert.AreEqual(expectedCount, this.chainblock.Count);
            //Assert.That(senders, Has.Member(expectedName));
            //Assert.AreEqual("Gosho", senders[0]);
        }

        [Test]
        public void GetAllSendersWithTransactionStatusShouldThrowInvalidOperationExceptionForNonExistantStatus()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Failed;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 * (i + 1);

                ITransaction currentT = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentT);
            }

            TransactionStatus fakeStatus = TransactionStatus.Successfull;

            Assert.That(() =>
            {
                this.chainblock.GetAllSendersWithTransactionStatus(fakeStatus);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.NonExistingTransactionWithGivenStatusMessage));
            //(ExceptionMessages.GetAllSendersWithTransactionStatusNonExistingStatusMessage));

            //ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            //for (int i = 0; i < 4; i++)
            //{
            //    int currentId = i + 1;
            //    TransactionStatus status = (TransactionStatus)i;
            //    string currentFrom = "Pesho" + i;
            //    string currentTo = "Gosho" + i;
            //    double currentAmount = 10;
            //    ITransaction currentTransaction = new Transaction
            //        (currentId, status, currentFrom, currentTo, currentAmount);

            //    if (status == TransactionStatus.Successfull)
            //    {
            //        expectedTransactions.Add(currentTransaction);
            //    }

            //    this.chainblock.Add(currentTransaction);
            //}

            //ITransaction transactionSuccess = new Transaction(5, TransactionStatus.Successfull, "Pesho4", "Gosho4", 15);
            //expectedTransactions.Add(transactionSuccess);
            //this.chainblock.Add(transactionSuccess);

            //TransactionStatus fakeStatus = TransactionStatus.Failed;

            //Assert.That(() =>
            //{
            //    IEnumerable<string> actualTransactionOutput = this.chainblock
            //        .GetAllSendersWithTransactionStatus(fakeStatus);
            //}, Throws.InvalidOperationException.With.Message.EqualTo
            //(ExceptionMessages.GetAllSendersWithTransactionStatusNonExistingStatusMessage));
        }

        [Test]
        public void GetAllReceiversWithTransactionStatusShouldReturnOrderedCollectionOfReceiversIfSuccessfull()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int currentId = i + 1;
                TransactionStatus status = (TransactionStatus)i;
                string currentFrom = "Pesho" + i;
                string currentTo = "Gosho" + i;
                double currentAmount = 10;
                ITransaction currentTransaction = new Transaction
                    (currentId, status, currentFrom, currentTo, currentAmount);

                if (status == TransactionStatus.Successfull)
                {
                    expectedTransactions.Add(currentTransaction);
                }

                this.chainblock.Add(currentTransaction);
            }

            ITransaction transactionSuccess = new Transaction(5, TransactionStatus.Successfull, "Pesho4", "Gosho4", 15);
            expectedTransactions.Add(transactionSuccess);
            this.chainblock.Add(transactionSuccess);

            IEnumerable<string> actualTransactionOutput = this.chainblock
                .GetAllReceiversWithTransactionStatus(TransactionStatus.Successfull);

            IEnumerable<string> expectedTransactionsOutput = expectedTransactions
                .OrderByDescending(t => t.Amount)
                .Select(t => t.To);

            CollectionAssert.AreEqual(expectedTransactionsOutput, actualTransactionOutput);
        }

        [Test]
        public void GetAllReceiversWithTransactionStatusShouldThrowInvalidOperationExceptionForNonExistantStatus()
        {
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Failed;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 * (i + 1);

                ITransaction currentT = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentT);
            }

            TransactionStatus fakeStatus = TransactionStatus.Successfull;

            Assert.That(() =>
            {
                this.chainblock.GetAllReceiversWithTransactionStatus(fakeStatus);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.NonExistingTransactionWithGivenStatusMessage));
           }

        //[Test]
        //public void GetAllOrderedByAmountDescendingThenByIdShouldReturnOrderedCollectionIfSuccessfull()
        //{
        //    ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

        //    for (int i = 0; i < 4; i++)
        //    {
        //        int currentId = i + 1;
        //        TransactionStatus status = (TransactionStatus)i;
        //        string currentFrom = "Pesho" + i;
        //        string currentTo = "Gosho" + i;
        //        double currentAmount = 10 * (i + 1);
        //        ITransaction currentTransaction = new Transaction
        //            (currentId, status, currentFrom, currentTo, currentAmount);
        //        expectedTransactions.Add(currentTransaction);
        //        this.chainblock.Add(currentTransaction);
        //    }
        //
        //    IEnumerable<ITransaction> actualTransactions = this.chainblock
        //        .GetAllOrderedByAmountDescendingThenById();

        //    expectedTransactions = expectedTransactions
        //        .OrderByDescending(t => t.Amount)
        //        .ThenBy(t => t.Id)
        //        .ToList();

        //    CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        //}

        [Test]
        public void GetAllOrderedByAmountDescendingThenByIdWithNoDuplicateAmounts()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            for (int i = 0; i < 10; i++)
            {
                int id = (i + 1);
                TransactionStatus status = (TransactionStatus)(i % 4);
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction =
                    new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
                expectedTransactions.Add(currentTransaction);
            }

            //expectedTransactions = this.chainblock
            //    .GetAllOrderedByAmountDescendingThenById()
            //    .ToList();

            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetAllOrderedByAmountDescendingThenById();

            expectedTransactions = expectedTransactions
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetAllOrderedByAmountDescendingThenByIdWithDuplicateAmounts()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            for (int i = 0; i < 10; i++)
            {
                int id = (i + 1);
                TransactionStatus status = (TransactionStatus)(i % 4);
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction =
                    new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
                expectedTransactions.Add(currentTransaction);
            }

            ITransaction transaction = new Transaction
                (11, TransactionStatus.Successfull, "Pesho11", "Gosho11", 10);

            this.chainblock.Add(transaction);
            expectedTransactions.Add(transaction);
            
            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetAllOrderedByAmountDescendingThenById();

            expectedTransactions = expectedTransactions
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetAllOrderedByAmountDescendingThenByIdWithEmptyCollection()
        {
            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetAllOrderedByAmountDescendingThenById();

            CollectionAssert.IsEmpty(actualTransactions);
        }

        [Test]
        public void GetBySenderOrderedByAmountDescendingShouldReturnOrderedCollectionIfSuccessfull()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            string sender = "Pesho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = sender;
                string to = "Gosho" + i;
                double amount = 10 + i;
                
                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);
                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho" + 1;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetBySenderOrderedByAmountDescending(sender);

            expectedTransactions = expectedTransactions
                .OrderByDescending(t => t.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);

            //ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            //for (int i = 1; i <= 5; i++)
            //{
            //    int id = (i + 1);
            //    TransactionStatus status = (TransactionStatus)(i % 4);
            //    string from = "Pesho";
            //    string to = "Gosho";
            //    double amount = 10 * i;

            //    ITransaction currentTransaction =
            //        new Transaction(id, status, from, to, amount);

            //    this.chainblock.Add(currentTransaction);
            //    expectedTransactions.Add(currentTransaction);
            //}

            //IEnumerable<ITransaction> actualTransactions = this.chainblock
            //    .GetBySenderOrderedByAmountDescending("Pesho");

            //expectedTransactions = expectedTransactions
            //    .Where(t => t.From == "Pesho")
            //    .OrderByDescending(t => t.Amount)
            //    .ToList();
        }

        [Test]
        public void GetBySenderOrderedByAmountDescendingShouldReturnInvalidOperationExceptionForNonExistantSender()
        {
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            string fakeSender = "Pesho";

            Assert.That(() =>
            {
                this.chainblock.GetBySenderOrderedByAmountDescending(fakeSender);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.GetBySenderOrderedByAmountDescendingNonExistingSenderMessage));
        }

        //[Test]
        //[TestCase("Unknown")]
        //[TestCase("")]
        //[TestCase(null)]
        //public void GetBySenderOrderedByAmountDescendingShouldReturnInvalidOperationExceptionForNonExistantSender(string fakeSender)
        //{
        //    for (int i = 1; i <= 5; i++)
        //    {
        //        int id = (i + 1);
        //        TransactionStatus status = (TransactionStatus)(i % 4);
        //        string from = "Pesho";
        //        string to = "Gosho";
        //        double amount = 10 * i;

        //        ITransaction currentTransaction =
        //            new Transaction(id, status, from, to, amount);

        //        this.chainblock.Add(currentTransaction);
        //    }

        //    Assert.That(() =>
        //    {
        //        this.chainblock.GetBySenderOrderedByAmountDescending(fakeSender);
        //    }, Throws.InvalidOperationException.With.Message.EqualTo
        //    (ExceptionMessages.GetBySenderOrderedByAmountDescendingNonExistingSenderMessage));
        //}

        [Test]
        public void GetByReceiverOrderedByAmountThenByIdWithoutDuplicatedAmountsShouldReturnOrderedCollectionIfSuccessfull()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            string receiver = "Gosho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho" + 1;
                string to = receiver;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);
                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetByReceiverOrderedByAmountThenById(receiver);

            expectedTransactions = expectedTransactions
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetByReceiverOrderedByAmountThenByIdWithDuplicatedAmountsShouldReturnOrderedCollectionIfSuccessfull()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();

            string receiver = "Gosho";

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho" + 1;
                string to = receiver;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);
                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            ITransaction tr = new Transaction
                (11, TransactionStatus.Successfull, "Pesho11", receiver, 10);
            // ???  Add to collections
            this.chainblock.Add(tr);
            expectedTransactions.Add(tr);

            IEnumerable<ITransaction> actualTransactions = this.chainblock
                .GetByReceiverOrderedByAmountThenById(receiver);

            expectedTransactions = expectedTransactions
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetByReceiverOrderedByAmountThenByIdShouldReturnInvalidOperationExceptionForNonExistantReceiver()
        {
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            string receiver = "Gosho";

            Assert.That(() =>
            {
                this.chainblock.GetByReceiverOrderedByAmountThenById(receiver);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.GetByReceiverOrderedByAmountThenByIdNonExistingReceiverMessage));

            //for (int i = 0; i < 4; i++)
            //{
            //    int id = i + 1;
            //    TransactionStatus status = TransactionStatus.Successfull;
            //    string from = "Pesho" + i;
            //    string to = "Gosho" + i;
            //    double amount = 10 + i;

            //    ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

            //    this.chainblock.Add(currentTransaction);
            //}

            //string fakeReceiver = "Pesho";

            //Assert.That(() =>
            //{
            //    this.chainblock.GetByReceiverOrderedByAmountThenById(fakeReceiver);
            //}, Throws.InvalidOperationException.With.Message.EqualTo
            //(ExceptionMessages.GetByReceiverOrderedByAmountThenByIdNonExistingReceiverMessage));
        }

        [Test]
        public void TestChainblockEnumerator()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            foreach (ITransaction transaction in this.chainblock)
            {
                actualTransactions.Add(transaction);
            }

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetByTransactionStatusAndMaximumAmountShouldReturnOrderedCollectionLessThanOrEqualToGivenMaximumAmount()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 1; i <= 4; i++)
            {
                int id = i;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho";
                double amount = 10 * i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            double maxAmount = 30;

            actualTransactions = this.chainblock
                .GetByTransactionStatusAndMaximumAmount(TransactionStatus.Successfull, maxAmount)
                .ToList();

            expectedTransactions = expectedTransactions
                .Where(t => t.Amount <= maxAmount)
                .Where(t => t.Status == TransactionStatus.Successfull)
                .OrderByDescending(t => t.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetByTransactionStatusAndMaximumAmountShouldReturnEmptyCollectionIfNoTransacationsRespondToStatus()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 1; i <= 4; i++)
            {
                int id = i;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho";
                double amount = 10 * i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            double maxAmount = 30;

            actualTransactions = this.chainblock
                .GetByTransactionStatusAndMaximumAmount(TransactionStatus.Failed, maxAmount)
                .ToList();

            expectedTransactions = expectedTransactions
                .Where(t => t.Amount <= maxAmount)
                .Where(t => t.Status == TransactionStatus.Failed)
                .OrderByDescending(t => t.Amount)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);

            // Alternative to CollectioAssert
            // OR
            //Assert.AreEqual(expectedTransactions.Count, actualTransactions.Count);
            //for (int i = 0; i < actualTransactions.Count; i++)
            //{
            //    Assert.That(actualTransactions.ToList()[i], Is.EqualTo(expectedTransactions.ToList()[i]));
            //}
        }

        [Test]
        public void GetByTransactionStatusAndMaximumAmountShouldReturnEmptyCollectionIfNoTransacationsRespondToMaxAmount()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 1; i <= 4; i++)
            {
                int id = i;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho";
                double amount = 10 * i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            double maxAmount = 0;

            actualTransactions = this.chainblock
                .GetByTransactionStatusAndMaximumAmount(TransactionStatus.Successfull, maxAmount)
                .ToList();

            expectedTransactions = expectedTransactions
                .Where(t => t.Amount <= maxAmount)
                .Where(t => t.Status == TransactionStatus.Failed)
                .OrderByDescending(t => maxAmount)
                .ToList();

            Assert.AreEqual(0, actualTransactions.Count);
            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetBySenderAndMinimumAmountDescendingShouldReturnOrderedCollectionWithTransactionsBiggerThanMinAmount()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 1; i <= 4; i++)
            {
                int id = i;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho";
                double amount = 10 * i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            double minAmount = 10;
            string sender = "Pesho";

            actualTransactions = this.chainblock
                .GetBySenderAndMinimumAmountDescending("Pesho", minAmount)
                .ToList();

            expectedTransactions = expectedTransactions
                .Where(t => t.From == sender)
                .Where(t => t.Amount > minAmount)
                .OrderByDescending(t => t.Amount)
                .ToList();

            Assert.AreEqual(3, expectedTransactions.Count);
            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        [TestCase("Gosho", 10)]
        [TestCase("Pesho", 100)]
        public void GetBySenderAndMinimumAmountDescendingShouldReturnInvalidOperationExceptionIfNoTransactionsRespondToSenderOrMinAmount(string sender, double minAmount)
        {
            for (int i = 1; i <= 4; i++)
            {
                int id = i;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho";
                double amount = 10 * i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.That(() =>
            {
                this.chainblock.GetBySenderAndMinimumAmountDescending(sender, minAmount);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.GetBySenderAndMinimumAmountDescendingNonExistingTransactionMessage));
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(20, 30)]
        [TestCase(10, 40)]
        [TestCase(100, 2000)]
        public void GetAllInAmountRangeShouldReturnCollectionWithTransactionsInRangeLoHiInclusive(double min, double max)
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 1; i <= 4; i++)
            {
                int id = i;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho";
                double amount = 10 * i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            actualTransactions = this.chainblock
                .GetAllInAmountRange(min, max)
                .ToList();

            expectedTransactions = expectedTransactions
                .Where(t => t.Amount >= min && t.Amount <= max)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);
        }

        [Test]
        public void GetByReceiverAndAmountRangeShouldReturnOrderedCollectionAccordingToReceiverAndGivenRange()
        {
            ICollection<ITransaction> expectedTransactions = new List<ITransaction>();
            ICollection<ITransaction> actualTransactions = new List<ITransaction>();

            for (int i = 1; i <= 4; i++)
            {
                int id = i;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho";
                double amount = 10 * i;

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                expectedTransactions.Add(currentTransaction);
                this.chainblock.Add(currentTransaction);
            }

            string receiver = "Gosho";
            double min = 10;
            double max = 40;


            actualTransactions = this.chainblock
                .GetByReceiverAndAmountRange(receiver, min, max)
                .ToList();

            expectedTransactions = expectedTransactions
                .Where(t => t.To == receiver)
                .Where(t => t.Amount >= min && t.Amount < max)
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Id)
                .ToList();

            CollectionAssert.AreEqual(expectedTransactions, actualTransactions);

            //Extra Testing if collection is ordered by amoubt descending
            //double testAmount = double.MaxValue;

            //foreach (var transaction in actualTransactions)
            //{
            //    Assert.That(transaction.Amount, Is.LessThan(testAmount)); or LessThanOrEqual !!!
            //    Assert.That(transaction.To, Is.EqualTo(receiver));
            //    Assert.That(transaction.Amount, Is.LessThan(max).And.GreaterThanOrEqualTo(min));

            //    testAmount = transaction.Amount;
            //}


            // With same amount, check if ordered by Id ascending
            //actualTransactions = this.chainblock
            //     .GetByReceiverAndAmountRange(receiver, min, max)
            //     .Where(t => t.Amount == 50)
            //     .ToList();

            //int id = int.MinValue;

            //foreach (var transaction in actualTransactions)
            //{
            //    Assert.That(transaction.Id, Is.GreaterThan(id));


            //    id = transaction.Id;
            //}
        }

        [Test]
        [TestCase("", 10, 40)]
        [TestCase("Gosho", 100, 400)]
        public void GetByReceiverAndAmountRangeShouldReturnExceptionIfNoTransactionsFound(string receiver, double min, double max)
        {
            string receiverName = "Gosho";

            for (int i = 1; i <= 4; i++)
            {
                int id = i;
                TransactionStatus status = TransactionStatus.Successfull;
                string from = "Pesho";
                string to = "Gosho";
                double amount = 10 * i;

                // Alternative for setting transaction status
                // TransactionStatus st = (TransactionStatus)(i % 4);

                // Alternative for setting receiver
                // string receiverInput = i % 2 == 0
                //    ? receiverName
                //    : i.ToString();

                ITransaction currentTransaction = new Transaction(id, status, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            Assert.That(() =>
            {
                this.chainblock
                .GetByReceiverAndAmountRange(receiver, min, max);
            }, Throws.InvalidOperationException.With.Message.EqualTo
            (ExceptionMessages.GetByReceiverAndAmountRangeNonExistingTransactionMessage));
        }
    }
}
