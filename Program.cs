using System;

namespace CoolBank
{
    internal class MainClass
    {

        static void Main(string[] args)
        {
            var account = new BankAccount("Shivam", 1000);
            Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} intial balance.");


            PaymentsByFriend();
            //PaymentByFriend(account);


            // Trying to open bank account with -ve balance

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


            // case Attemp to overdraw

            try
            {
                account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine("Exception caught trying to overdraw");
                Console.WriteLine(e.ToString());
            }


            TransactionHistory();

            Console.ReadLine();
        }

        // Client Payments
        internal static void PaymentsByFriend()
        {
            var account = new BankAccount("Shivam", 1000);
            account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
            Console.WriteLine(account.Balance);
            account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
            Console.WriteLine(account.Balance);
        }

        internal static void TransactionHistory()
        {
            var account = new BankAccount("Shivam", 1000);
            Console.WriteLine(account.GetAccountHistory());
        }

    }



}
