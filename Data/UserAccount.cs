using System.Collections.Generic;

namespace SimpleATM.Data
{
    public class UserAccount
    {
        public string CardNumber { get; set; }
        public string PIN { get; set; }
        public decimal Balance { get; set; }
        public List<string> Transactions { get; set; }
        public int TransactionCountToday { get; set; }
        public DateTime LastTransactionDate { get; set; }

        public UserAccount(string cardNumber, string pin, decimal balance)
        {
            CardNumber = cardNumber;
            PIN = pin;
            Balance = balance;
            Transactions = new List<string>();
            TransactionCountToday = 0;
            LastTransactionDate = DateTime.Now;
        }

        public void AddTransaction(string transaction)
        {
            if (Transactions.Count >= 5)
            {
                Transactions.RemoveAt(0);
            }
            Transactions.Add(transaction);
            LastTransactionDate = DateTime.Now;
            TransactionCountToday++;
        }
    }
}
