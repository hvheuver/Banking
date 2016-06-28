using System;

namespace Banking.Models
{
    public class Transaction
    {
        #region Properties
        public DateTime DateOfTrans { get; private set; }
        public TransactionType TransactionType { get; private set; }
        public decimal Amount { get; private set; }
        #endregion

        #region Constructors
        public Transaction(decimal amount, TransactionType type)
        {
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