using Banking.Models.Domain;
using System;
using Xunit;

namespace Banking.Tests.Models.Domain
{
    public class BankAccountTest2
    {
        private readonly BankAccount _bankAccount;

        public BankAccountTest2()
        {
            string accountNumber = "123-4567890-02";
            _bankAccount = new BankAccount(accountNumber);
        }

        [Theory]
        [InlineData("123-4567890-0333")] //too long
        [InlineData("123-1547563@60")] //wrong format
        [InlineData("123-4567890-03")] //not divisable by 97
        public void NewAccount_WrongAccountNumber_Fails(string accountNumber)
        {
            Assert.Throws<ArgumentException>(() => new BankAccount(accountNumber));
        }

        [Fact]
        public void NewAccount_Null_Fails()
        {
            Assert.Throws<ArgumentNullException>(() => new BankAccount(null));
        }

        [Fact]
        public void NewAccount_ToLong_Fails()
        {
            string number = "123-4567890-0333";
            Assert.Throws<ArgumentException>(() => new BankAccount(number));
        }

        [Fact]
        public void NewAccount_WrongFormat_Fails()
        {
            string number = "123-1547563@60";
            Assert.Throws<ArgumentException>(() => new BankAccount(number));
        }

        [Fact]
        public void NewAccount_NoDivisionBy97_Fails()
        {
            string number = "123-4567890-03";
            Assert.Throws<ArgumentException>(() => new BankAccount(number));
        }

        [Fact]
        public void Deposit_AmountBiggerThanZero_ChangesBalance()
        {
            _bankAccount.Deposit(200);
            _bankAccount.Deposit(400);
            Assert.Equal(600, _bankAccount.Balance);
        }

        [Theory]
        [InlineData(200, 110, 90)]
        [InlineData(200, 300, -100)]
        public void Withdraw_AmountBiggerThanZero_ChangesBalance(decimal depositAmount, decimal withdrawAmount, decimal expected)
        {
            _bankAccount.Deposit(depositAmount);
            _bankAccount.Withdraw(withdrawAmount);
            Assert.Equal(expected, _bankAccount.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Withdraw_NegativeOrZeroAmount_Fails(decimal amount)
        {
            Assert.Throws<ArgumentException>(() => _bankAccount.Withdraw(amount));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-100)]
        public void Deposit_NegativeOrZeroAmount_Fails(decimal amount)
        {
            Assert.Throws<ArgumentException>(() => _bankAccount.Deposit(amount));
        }

        [Fact]
        public void Equals_2BankAccountsWithSameAccountNumber_ReturnsTrue()
        {
            BankAccount bankAccount2 = new BankAccount(_bankAccount.AccountNumber);
            Assert.True(_bankAccount.Equals(bankAccount2));
        }

        [Fact]
        public void Equals_2BankAccountsWithDifferentAccountNumber_ReturnsFalse()
        {
            BankAccount bankAccount2 = new BankAccount("123-4567891-03");
            Assert.False(_bankAccount.Equals(bankAccount2));
        }
    }
}
