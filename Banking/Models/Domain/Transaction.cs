using System;

namespace Banking.Models.Domain
{
    public class Transaction
    {
        #region Properties
        public DateTime DateOfTrans { get; }
        public TransactionType TransactionType { get; }
        public decimal Amount { get; }
        #endregion

        #region Constructors
        public Transaction(decimal amount, TransactionType type)
        {
            if (amount <= 0)
               throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be positive");
            Amount = amount;
            TransactionType = type;
            DateOfTrans = DateTime.Today;
        }
        #endregion

        #region Methods
        public bool IsWithdraw
        {
            get { return TransactionType == TransactionType.Withdraw; }
        }

        public bool IsDeposit
        {
            get { return TransactionType == TransactionType.Deposit; }
        }
        #endregion
    }
}

