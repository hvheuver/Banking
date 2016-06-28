using System;

namespace Banking.Models
{
    public class BankAccount
    {
        #region Fields
        private string _accountNumber;
        #endregion

        #region Properties
        public decimal Balance { get; private set; }

        public string AccountNumber
        {
            get { return _accountNumber; }
            set { _accountNumber = value; }
        }
        #endregion

        #region Constructors
        public BankAccount(string account)
        {
            AccountNumber = account;
            Balance = Decimal.Zero;
        }
        #endregion

        #region Methods
        public void Withdraw(decimal amount)
        {
            Balance -= amount;
        }

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }
        #endregion
    }
}
