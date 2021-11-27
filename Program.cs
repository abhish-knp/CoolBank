using System;
using System.Collections.Generic;

namespace CoolBank
{
    internal class MainClass
    {

        static void Main(string[] args)
        {
            var account = new BankAccount("Shivam", 1000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} intial balance.");

            {
                account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
                Console.WriteLine(account.Balance);
                account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
                Console.WriteLine(account.Balance);
            }

            // Trying to open bank account with -ve balance
            {
                BankAccount invalidAccount;
                try
                {
                    invalidAccount = new BankAccount("invalid", -55);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Exception caught creating account with negative balance");
                    Console.WriteLine(e.ToString());
                    return;
                }
            }


            {
                try
                {
                    account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine("Exception caught trying to overdraw");
                    Console.WriteLine(e.ToString());
                }
            }

            // Transaction History
            {
                Console.WriteLine(account.GetAccountHistory());
            }

            Console.ReadLine();
        }
    }

    public class BankAccount
    {
        private static int accountNumberSeed = 1234567000;
        /// <summary>
        /// There are three properties and two methods.
        /// </summary>
        public string Number { get; }
        public string Name { get; set; }
        public string Owner { get; }
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;
                }

                return balance;
            }
        }

        private List<Transaction> allTransactions = new List<Transaction>();

        public BankAccount(string name, decimal intialBalance)
        {

            this.Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            this.Owner = name;
            MakeDeposit(intialBalance, DateTime.Now, "Intial balance ");
        }

        

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Date\t\tAmount\tBalance\tNote");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
            }

            return report.ToString();
        }

        /*
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {

        }

        public void makeWithdrawal(decimal amount, DateTime date, string note)
        {

        }
        */

    }

    public class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public String Notes { get; }

        public Transaction(decimal amount, DateTime date, string notes)
        {
            this.Amount = amount;
            this.Notes = notes;
            this.Date = date;

        }
    }
}
