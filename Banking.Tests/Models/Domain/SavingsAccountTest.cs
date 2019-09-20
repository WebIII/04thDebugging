using Banking.Models.Domain;
using System;
using Xunit;

namespace Banking.Tests.Models.Domain
{
    public class SavingsAccountsTests
    {
        private static readonly string _savingsAccountNumber = "123-4567891-03";
        private readonly SavingsAccount _savingsAccount;

        public SavingsAccountsTests()
        {
            _savingsAccount = new SavingsAccount(_savingsAccountNumber, 0.02M);
            _savingsAccount.Deposit(200);
        }

        [Fact]
        public void NewSavingsAccount_SetsInterestRate()
        {
            Assert.Equal(0.02M, _savingsAccount.InterestRate);
        }

        [Fact]
        public void Withdraw_Amount_AddsCosts()
        {
            _savingsAccount.Withdraw(100);
            Assert.True(_savingsAccount.Balance < 100);
        }

        [Fact]
        public void Withdraw_Amount_CausesTwoTransactions()
        {
            _savingsAccount.Withdraw(100);
            Assert.Equal(3, _savingsAccount.NumberOfTransactions);
        }

        [Fact]
        public void Withdraw_IfBalanceGetsNegative_Fails()
        {
            Assert.Throws<InvalidOperationException>(() => _savingsAccount.Withdraw(200));
        }

        [Fact]
        public void AddInterest_ChangesBalance()
        {
            _savingsAccount.AddInterest();
            Assert.Equal(204, _savingsAccount.Balance);
        }
    }
}