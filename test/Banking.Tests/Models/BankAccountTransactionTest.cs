using System.Collections.Generic;
using System.Linq;
using Banking.Models;
using System;
using Xunit;

namespace Banking.Tests.Models
{
    public class BankAccountTransactionTest
    {
        private static string _bankAccountNumber = "123-4567890-02";
        private BankAccount _bankAccount;
        private DateTime _yesterday = DateTime.Today.AddDays(-1);
        private DateTime _tomorrow = DateTime.Today.AddDays(1);

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
            Transaction t = _bankAccount.GetTransactions(DateTime.Today, DateTime.Today).ToArray()[0];
            Assert.Equal(100, t.Amount);
            Assert.Equal(TransactionType.Deposit, t.TransactionType);
        }

        [Fact]
        public void WithDraw_Amount_AddsTransaction()
        {
            _bankAccount.Withdraw(100);
            Assert.Equal(1, _bankAccount.NumberOfTransactions);
            //Test of de toegevoegde transactie de juiste gegevens bevat
            Transaction t = _bankAccount.GetTransactions(DateTime.Today, DateTime.Today).ToArray()[0];
            Assert.Equal(100, t.Amount);
            Assert.Equal(TransactionType.Withdraw, t.TransactionType);
        }


        [Fact]
        public void GetTransactions_NoParameters_ReturnsAllTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            Transaction[] t = _bankAccount.GetTransactions(null, null).ToArray();
            Assert.Equal(2, t.Length);
        }

        [Fact]
        public void GetTransactions_NoTransactions_ReturnsEmptyList()
        {
            Transaction[] t = _bankAccount.GetTransactions(null, null).ToArray();
            Assert.Equal(0, t.Length);
        }

        [Fact]
        public void GetTransactions_WithinAPeriodThatHasTransactions_ReturnsTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            Transaction[] t = _bankAccount.GetTransactions(_yesterday, _tomorrow).ToArray();
            Assert.Equal(2, t.Length);
        }

        [Fact]
        public void GetTransactions_WithinAPeriodThatHasNoTransactions_ReturnsNoTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            Transaction[] t = _bankAccount.GetTransactions(_yesterday, _yesterday).ToArray();
            Assert.Equal(0, t.Length);
        }

        [Fact]
        public void GetTransactions_BeforeADateWithTransactions_ReturnsTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            Transaction[] t = _bankAccount.GetTransactions(null, _tomorrow).ToArray();
            Assert.Equal(2, t.Length);
        }

        [Fact]
        public void GetTransactions_BeforeADateWithoutTransactions_ReturnsNoTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            Transaction[] t = _bankAccount.GetTransactions(null, _yesterday).ToArray();
            Assert.Equal(0, t.Length);
        }

        [Fact]
        public void GetTransactions_AfterADateWithTransactions_ReturnsTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            List<Transaction> t = _bankAccount.GetTransactions(_yesterday, null).ToList();
            // vb. gebruik van ToList en de Count property
            Assert.Equal(2, t.Count);
        }

        [Fact]
        public void GetTransactions_AfterADateWithoutTransactions_ReturnsNoTransactions()
        {
            _bankAccount.Deposit(100);
            _bankAccount.Deposit(100);
            IEnumerable<Transaction> t = _bankAccount.GetTransactions(_tomorrow, null).ToArray();
            // vb gebruik maken van de extension method Count() op IEnumerable
            Assert.Equal(0, t.Count());
        }


    }
}

