using System;
using System.Diagnostics;
using SimpleATM.Data;
using SimpleATM.Services;

namespace SimpleATM
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Simple ATM!");

            Console.Write("Enter your card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter your PIN: ");
            string pin = Console.ReadLine();

            // Authenticate the user
            UserAccount? loggedInUser = Authentication.VerifyUser(cardNumber, pin);

            if (loggedInUser != null)
            {
                Console.WriteLine("Login successful!");
                ShowMainMenu(loggedInUser);
            }
            else
            {
                Console.WriteLine("Invalid card number or PIN. Exiting...");
            }
        }

        static void ShowMainMenu(UserAccount user)
        {
            while (true)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. View Last Five Transactions");
                Console.WriteLine("3. Withdraw Cash");
                Console.WriteLine("4. Fast Cash Withdraw");
                Console.WriteLine("5. Change PIN");
                Console.WriteLine("6. View Detailed Bank Statement");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CheckBalance(user);
                        break;
                    case "2":
                        ViewTransaction(user);
                        break;
                    case "3":
                        WithdrawCash(user);
                        break;
                    case "4":
                        FastWithdrawCash(user);
                        break;
                    case "5":
                        ChangePIN(user);
                        break;
                    case "6":
                        ViewDetailedStatement(user);
                        break;
                    case "7":
                        Console.WriteLine("Thank you for using Simple ATM. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CheckBalance(UserAccount user)
        {
            Console.WriteLine($"\nYour current balance is: ${user.Balance:F2}");
        }

        static void ViewTransaction(UserAccount user)
        {
            Console.WriteLine("\n--- Last Five Transactions ---");
            if (user.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions available.");
            }
            else
            {
                for (int i = user.Transactions.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine(user.Transactions[i]);
                }
            }
        }

        static void WithdrawCash(UserAccount user)
        {
            if (user.TransactionCountToday >= 10)
            {
                Console.WriteLine("You have reached the maximum number of transactions for today.");
                return;
            }

            Console.Write("Enter the amount to withdraw: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            if (amount > 1000)
            {
                Console.WriteLine("You can only withdraw up to $1,000 in a single transaction.");
                return;
            }

            if (amount > user.Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            user.Balance -= amount;
            user.AddTransaction($"Withdrew ${amount:F2}");

            Console.WriteLine($"Successfully withdrew ${amount:F2}. Your new balance is ${user.Balance:F2}");
        }

        static void FastWithdrawCash(UserAccount user)
        {
            Console.WriteLine("\n--- Fast Cash Withdrawal ---");
            Console.WriteLine("1. $20");
            Console.WriteLine("2. $50");
            Console.WriteLine("3. $100");
            Console.WriteLine("4. Cancel");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            decimal amount = 0;

            switch (choice)
            {
                case "1":
                    amount = 20;
                    break;
                case "2":
                    amount = 50;
                    break;
                case "3":
                    amount = 100;
                    break;
                case "4":
                    Console.WriteLine("ITransaction cancelled.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    return;
            }

            if (user.TransactionCountToday >= 10)
            {
                Console.WriteLine("You have reached the maximum number of transactions for today.");
                return;
            }

            if (amount > user.Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            user.Balance -= amount;
            user.AddTransaction($"Withdrew ${amount:F2}");
            Console.WriteLine($"Successfully withdrew ${amount:F2}. Your new balance is ${user.Balance:F2}");
        }

        static void ChangePIN(UserAccount user)
        {
            Console.Write("\n--- Change PIN ---");
            Console.Write("\nEnter your current PIN: ");
            string currentPin = Console.ReadLine();

            if (currentPin != user.PIN)
            {
                Console.WriteLine("Invalid PIN.");
                return;
            }

            Console.Write("Enter your new PIN: ");
            string newPin = Console.ReadLine();

            if (newPin.Length != 4 || !int.TryParse(newPin, out _))
            {
                Console.WriteLine("PIN must be exactly 4 digits.");
                return;
            }

            Console.Write("Confirm your new PIN: ");
            string confirmPin = Console.ReadLine();

            if (newPin != confirmPin)
            {
                Console.WriteLine("PINs do not match.");
                return;
            }

            user.PIN = newPin;
            Console.WriteLine("PIN changed successfully.");
        }

        static void ViewDetailedStatement(UserAccount user)
        {
            Console.WriteLine("\n--- Detailed Bank Statement ---");

            if (user.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions found.");
            }
            else
            {
                foreach (var transaction in user.Transactions)
                {
                    Console.WriteLine(transaction);
                }
            }
        }
    }
}
