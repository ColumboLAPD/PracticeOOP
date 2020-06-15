using System;
using BankLibrary;

namespace PracticeOOP
{
    class Program
    {
        //commit
        static void Main(string[] args)
        {
            Bank<Account> bank = new Bank<Account>("Sberbank");
            bool alive = true;
            while(alive)
            {                
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("1. Open account\t 2. Withdraw money\t 3. Put in account");
                Console.WriteLine("4. Close account\t 5. Skip day\t 6. Close the program");
                Console.Write("Enter position: ");
                Console.ForegroundColor = color;
                try
                {
                    int command = Convert.ToInt32(Console.ReadLine());
                    switch(command)
                    {
                        case 1:
                            OpenAccount(bank);
                            break;
                        case 2:
                            WithdrawF(bank);
                            break;
                        case 3:
                            Put(bank);
                            break;
                        case 4:
                            CloseAccount(bank);
                            break;
                        case 5:
                            break;
                        case 6:
                            alive = false;
                            continue;
                    }
                    bank.CalculateP();
                }
                catch(Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = color;
                }
            }
        }
        private static void OpenAccount(Bank<Account> bank)
        {
            Console.Write("Enter sum for open account: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Enter type account: 1 - demand, 2 - deposit");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());
            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            bank.Open(accountType, sum, AddSum, WithDraw,
                (o, e) => Console.WriteLine(e.Message), CloseAcc, OpenAccount);
        }
        private static void WithdrawF(Bank<Account> bank)
        {
            Console.Write("Enter sum for withdraw: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter id account: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Withdraw(sum, id);
        }
        private static void Put(Bank<Account> bank)
        {
            Console.Write("Enter sum for put: ");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter id account: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Put(sum, id);
        }
        private static void CloseAccount(Bank<Account> bank)
        {
            Console.Write("Enter id account for close: ");
            int id = Convert.ToInt32(Console.ReadLine());

            bank.Close(id);
        }
        private static void OpenAccount(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void AddSum(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
        private static void WithDraw(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
            if (e.Sum > 0)
                Console.WriteLine("Go to shop");
        }
        private static void CloseAcc(object sender, AccountEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
