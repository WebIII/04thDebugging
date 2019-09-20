using System;

namespace Banking.Models.Domain
{
    public class SavingsAccount : BankAccount
    {
        #region Fields
        protected const decimal WithdrawCost = 0.25;
        #endregion

        #region Properties
        public decimal InterestRate { get; }
        #endregion

        #region Constructors
        public SavingsAccount(string bankAccountNumber, decimal interestRate)
            : base(bankAccountNumber)
        {
            InterestRate = interestRate;
        }
        #endregion

        #region Methods
        public override void Withdraw(decimal amount)
        {
            if (Balance - amount - WithdrawCost < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            base.Withdraw(amount);
            base.Withdraw(WithdrawCost);
        }

        public void AddInterest()
        {
            Deposit(Balance * InterestRate);
        }
        #endregion
    }
}
