using System;
using System.Collections.Generic;
using SimpleATM.Data;

namespace SimpleATM.Services
{
    public static class Authentication
    {
        private static List<UserAccount> userAccounts = new List<UserAccount>
        {
            new UserAccount("123456", "1111", 5000.00m),
            new UserAccount("654321", "2222", 3000.00m)
        };

        public static UserAccount? VerifyUser(string cardNumber, string pin)
        {
            foreach (var account in userAccounts)
            {
                if (account.CardNumber == cardNumber && account.PIN == pin)
                {
                    return account;
                }
            }
            return null;
        }
    }
}