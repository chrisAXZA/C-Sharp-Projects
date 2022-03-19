namespace Chainblock.Models
{
    using System;
    
    using Chainblock.Common;
    using Chainblock.Contracts;

    public class Transaction : ITransaction
    {
        private const int MIN_ID_VALUE = 0;
        private const double MIN_AMOUNT_VALUE = 0.0;

        private int id;
        private string to;
        private string from;
        private double amount;
        private TransactionStatus status;

        public Transaction(int id, TransactionStatus transactionStatus,
            string from, string to, double amount)
        {
            this.Id = id;
            this.Status = transactionStatus;
            this.From = from;
            this.To = to;
            this.Amount = amount;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (value <= MIN_ID_VALUE)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidIdMessage);
                }
                this.id = value;
            }
        }
        
        // Also variant
        //public TransactionStatus Status { get; set; }
        public TransactionStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        public string From
        {
            get
            {
                return this.from;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidSenderUsernameMessage);
                }
                this.from = value;
            }
        }

        public string To
        {
            get
            {
                return this.to;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidReceiverUsernameMessage);
                }
                this.to = value;
            }
        }

        public double Amount
        {
            get
            {
                return this.amount;
            }
            set
            {
                if (value <= MIN_AMOUNT_VALUE)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTransactionAmountMessage);
                }
                this.amount = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ITransaction transaction)
            {
                return this.Id == transaction.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
