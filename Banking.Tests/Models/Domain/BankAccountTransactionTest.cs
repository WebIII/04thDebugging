using Banking.Models.Domain;
using System;
using System.Collections.Generic;
using Xunit;

namespace Banking.Tests.Models.Domain
{
    public class BankAccountTransactionTest
    {
        private static readonly string _bankAccountNumber = "123-4567890-02";
        private readonly BankAccount _bankAccount;
        private readonly DateTime _tomorrow = DateTime.Today.AddDays(1);

        public BankAccountTransactionTest()
        {
            _bankAccount = new BankAccount(_bankAccountNumber);
        }

        [Fact]
        public void NewAccount_HasZeroTransactions()
        {
            Assert.Equal(0, _bankAccount.NumberOfTransactions);
        }

        [Fact]
        public void Deposit_Amount_AddsTransaction()
        {
            _bankAccount.Deposit(100);
            Assert.Equal(1, _bankAccount.NumberOfTransactions);
            //Test of de toegevoegde transactie de juiste gegevens bevat
            Transaction t = new List<Transaction>(_bankAccount.GetTransactions(DateTime.Today, DateTime.Today))[0];
            Assert.Equal(100, t.Amount);
            Assert.Equal(TransactionType.Deposit, t.TransactionType);
        }

        [Fact]
        public void GetTransactions_AfterADateWithoutTransactions_ReturnsNoTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(_tomorrow, null));
            Assert.Empty(tt);
        }

        public static IEnumerable<Object[]> TestData
        {
            get
            {
                DateTime yesterday = DateTime.Today.AddDays(-1);
                DateTime tomorrow = DateTime.Today.AddDays(1);

                yield return new Object[] { null, null, 2 };//All
                yield return new Object[] { yesterday, tomorrow, 2 };//WithinAPeriodThatHasTransactions
                yield return new Object[] { yesterday, yesterday, 0 };//WithinAPeriodThatHasNoTransactions
                yield return new Object[] { null, tomorrow, 2 };//BeforeADateWithTransactions
                yield return new Object[] { null, yesterday, 0 };//BeforeADateWithoutTransactions
                yield return new Object[] { yesterday, null, 2 };//AfterADateWithTransactions
                yield return new Object[] { tomorrow, null, 0 }; //AfterADateWithoutTransactions
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void GetTransactions_ReturnsTransactions(DateTime? from, DateTime? till, int expected)
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(from, till));
            Assert.Equal(expected, tt.Count);
        }
    }
}

