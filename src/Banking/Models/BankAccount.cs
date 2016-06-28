﻿using System;
using System.Collections.Generic;

namespace Banking.Models
{
    public class BankAccount: IBankAccount
    {
        #region Fields
        private string _accountNumber;
        private IList<Transaction> _transactions;
        #endregion

        #region Properties
        public decimal Balance { get; private set; }

        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }

        public int NumberOfTransactions
        {
            get { return _transactions.Count; }
        }
        #endregion

        #region Constructors
        public BankAccount(string account)
        {
            AccountNumber = account;
            Balance = Decimal.Zero;
            _transactions = new List<Transaction>();
        }
        #endregion

        #region Methods
        public virtual void Withdraw(decimal amount)
        {
            _transactions.Add(new Transaction(amount, TransactionType.Withdraw));
            Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            _transactions.Add(new Transaction(amount, TransactionType.Deposit));
            Balance += amount;
        }

        public IEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? till)
        {
            IList<Transaction> transList = new List<Transaction>();
            foreach (Transaction t in _transactions)
                if (t.DateOfTrans >= from && t.DateOfTrans <= till)
                    transList.Add(t);
            return transList;
        }

        public override string ToString()
        {
            return $"{AccountNumber} - {Balance}";
        }

        public override bool Equals(object obj)
        {
            BankAccount account = obj as BankAccount;
            if (account == null) return false;
            return AccountNumber == account.AccountNumber;
        }

        public override int GetHashCode()
        {
            return AccountNumber?.GetHashCode() ?? 0;
        }
        #endregion
    }
}
