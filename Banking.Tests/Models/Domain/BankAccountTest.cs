using Banking.Models.Domain;
using System;
using Xunit;

namespace Banking.Tests.Models.Domain
{
    public class BankAccountTest
    {
        private readonly BankAccount _bankAccount;
        private readonly string _accountNumber;

        public BankAccountTest()
        {
            _accountNumber = "123-4567890-02";
            _bankAccount = new BankAccount(_accountNumber);
        }

        [Fact]
        public void NewAccount_BalanceZero()
        {
            //Assert
            Assert.Equal(0, _bankAccount.Balance);
        }

        [Fact]
        public void NewAccount_SetsAccountNumber()
        {
            Assert.Equal(_accountNumber, _bankAccount.AccountNumber);
        }

        [Fact]
        public void NewAccount_EmptyString_Fails()
        {
            Assert.Throws<ArgumentException>(
                () => new BankAccount(String.Empty));
        }

    }
}
