namespace Chainblock.Core
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    
    using Chainblock.Common;
    using Chainblock.Contracts;

    public class Chainblock : IChainblock
    {
        private ICollection<ITransaction> transactions;

        public Chainblock()
        {
            this.transactions = new List<ITransaction>();
        }

        public int Count => this.transactions.Count;

        public void Add(ITransaction tx)
        {
            // Transaction should also be checked if not null
            //if (tx == null)
            //{
            //    throw new ArgumentException
            //        ();
            //}

            if (this.Contains(tx))
            {
                throw new InvalidOperationException
                    (ExceptionMessages.InvalidAddExistingTransactionMessage);
            }

            // Not necessary since both Contains are linked between each other
            //if (this.Contains(tx.Id))
            //{
            //    throw new InvalidOperationException
            //        (ExceptionMessages.InvalidAddExistingTransactionMessage);
            //}

            this.transactions.Add(tx);
        }

        public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
        {
            //ITransaction transaction;

            //transaction = this.GetById(id);

            //if (transaction == null)
            //{
            //    throw new ArgumentException
            //        (ExceptionMessages.ChangeStatusOfNonExistingTransactionMessage);
            //}

            ITransaction transaction = this.transactions.FirstOrDefault(t => t.Id == id);

            if (transaction == null)
            {
                throw new ArgumentException
                    (ExceptionMessages.ChangeStatusOfNonExistingTransactionMessage);
            }

            transaction.Status = newStatus;
        }

        public bool Contains(ITransaction tx)
        {
            return this.Contains(tx.Id);
        }

        public bool Contains(int id)
        {
            bool transactionExists = this.transactions.Any(t => t.Id == id);
            return transactionExists;
        }

        public IEnumerable<ITransaction> GetAllInAmountRange(double lo, double hi)
        {
            return this.transactions.Where(t => t.Amount >= lo && t.Amount <= hi);
        }

        public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
        {
            IEnumerable<ITransaction> transactionsTarget = this.transactions
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Id);

            return transactionsTarget;
        }

        public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> receivers = this.GetByTransactionStatus(status)
                .Select(t => t.To);

            return receivers;
        }

        public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> senders = this.GetByTransactionStatus(status)
                .Select(t => t.From);

            //if (senders.Count() == 0)
            //{
            //    throw new InvalidOperationException
            //        (ExceptionMessages.GetAllSendersWithTransactionStatusNonExistingStatusMessage);
            //}

            return senders;
            
            //IEnumerable<string> senders = this.transactions.OrderByDescending(t => t.Amount)
            //    .Where(t => t.Status == status)
            //    .Select(t => t.From);

            //if (senders.Count() == 0)
            //{
            //    throw new InvalidOperationException
            //        (ExceptionMessages.GetAllSendersWithTransactionStatusNonExistingStatusMessage);
            //}

            //return senders;
        }

        public ITransaction GetById(int id)
        {
            //    ITransaction transaction = this.transactions.FirstOrDefault(t => t.Id == id);

            //    if (transaction == null)
            //    {
            //        throw new InvalidOperationException
            //             (ExceptionMessages.GetIdOfNonExistingTransactionMessage);
            //    }

            // OR
            // return this.transactions[id]; => possible if working with Dictionary

            // OR
            // if(!this.Transactions.Contains(id))
            if (!this.transactions.Any(t => t.Id == id))
            {
                throw new InvalidOperationException
                    (ExceptionMessages.GetIdOfNonExistingTransactionMessage);
            }

            return this.transactions.First(t => t.Id == id);
        }

        public IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
        {
            IEnumerable<ITransaction> transactionsTarget = this.transactions
                .Where(t => t.To == receiver) // Where(t => t.To == receiver && t => t.Amount >= lo && t.Amount < hi) ALSO POSSIBLE
                .Where(t => t.Amount >= lo && t.Amount < hi)
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Id);

            if (transactionsTarget.Count() == 0)
            {
                throw new InvalidOperationException
                    (ExceptionMessages.GetByReceiverAndAmountRangeNonExistingTransactionMessage);
            }

            return transactionsTarget;
        }

        public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
        {
            IEnumerable<ITransaction> transactionsTarget = this.transactions
                .Where(t => t.To == receiver)
                .OrderByDescending(t => t.Amount)
                .ThenBy(t => t.Id);

            if (transactionsTarget.Count() == 0)
            {
                throw new InvalidOperationException
                    (ExceptionMessages.GetByReceiverOrderedByAmountThenByIdNonExistingReceiverMessage);
            }

            return transactionsTarget;
        }

        public IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
        {
            IEnumerable<ITransaction> transactionsTarget = this.transactions
                 .Where(t => t.From == sender)
                 .Where(t => t.Amount > amount)
                 .OrderByDescending(t => t.Amount);

            if (transactionsTarget.Count() == 0)
            {
                throw new InvalidOperationException
                    (ExceptionMessages.GetBySenderAndMinimumAmountDescendingNonExistingTransactionMessage);
            }

            return transactionsTarget;
        }

        public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
        {
            IEnumerable<ITransaction> transactionsTarget = this.transactions
                .Where(t => t.From == sender)
                .OrderByDescending(t => t.Amount);

            if (transactionsTarget.Count() == 0)
            {
                throw new InvalidOperationException
                    (ExceptionMessages.GetBySenderOrderedByAmountDescendingNonExistingSenderMessage);
            }

            return transactionsTarget;
        }

        public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
        {
            // OR for Dictionary
            // var filteredTransactions = this.transactions.Values
            //          .Where(v => v.Status == status)
            //          .OrderByDescending(t => t.Amount);

            IEnumerable<ITransaction> transactions = this.transactions
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.Amount);

            if (transactions.Count() == 0)
            {
                throw new InvalidOperationException
                    (ExceptionMessages.NonExistingTransactionWithGivenStatusMessage);
            }

            return transactions;

            //if (!this.transactions.Any(t => t.Status == status))
            //{
            //    throw new InvalidOperationException(ExceptionMessages.NonExistingTransactionWithGivenStatusMessage);
            //}

            //return this.transactions.Where(t => t.Status == status).OrderByDescending(t => t.Amount);
        }

        public IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
        {
            return this.transactions
                .Where(t => t.Amount <= amount)
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.Amount);
        }

        public void RemoveTransactionById(int id)
        {
            //ITransaction transaction = this.transactions.FirstOrDefault(t => t.Id == id);
            //if (transaction == null)
            //{
            //    throw new InvalidOperationException
            //        (ExceptionMessages.RemoveNonExistingTransactionTransactionMessage);
            //}

            try
            {
            ITransaction transaction = this.GetById(id);

            this.transactions.Remove(transaction);
            }
            catch (InvalidOperationException ioe)
            {
                throw new InvalidOperationException
                    (ExceptionMessages.RemoveNonExistingTransactionMessage, ioe);
            }
        }

        public IEnumerator<ITransaction> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this.transactions.ToArray() [i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
