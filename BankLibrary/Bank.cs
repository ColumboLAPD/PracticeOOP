using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
    //обобщенный класс для создания и работы со счетами
    public class Bank<T> where T : Account
    {
        T[] accounts;
        public string Name { get; set; }
        public Bank(string _name)
        {
            Name = _name;
        }
        public void Open(AccountType accountType, decimal sum,
            AccountStateHandler add, AccountStateHandler withdraw,
            AccountStateHandler open, AccountStateHandler close,
            AccountStateHandler calculate)
        {
            T newAccount = null;

            switch(accountType)
            {
                case AccountType.Ordinary:
                    newAccount = new DemandAccount(sum, 10) as T;
                    break;
                case AccountType.Deposit:
                    newAccount = new DepositAccount(sum, 20) as T;
                    break;
            }

            if (newAccount == null)
                throw new Exception("Error create account!");
            if (accounts == null)
                accounts = new T[] { newAccount };
            else
            {
                T[] tempAccounts = new T[accounts.Length + 1];
                for (int i = 0; i < accounts.Length; ++i)
                    tempAccounts[i] = accounts[i];
                tempAccounts[tempAccounts.Length] = newAccount;
                accounts = tempAccounts;
            }

            newAccount.Added += add;
            newAccount.Calculated += calculate;
            newAccount.Closed += close;
            newAccount.Opened += open;
            newAccount.WithDraw += withdraw;

            newAccount.Open();
        }
        public T FindAccount(int id)
        {
            for (int i = 0; i < accounts.Length; ++i)
            {
                if (accounts[i].Id == id)
                    return accounts[i];
            }
            return null;
        }
        public T FindAccount(int id, out int index)
        {
            for (int i = 0; i < accounts.Length; ++i)
            {
                if(accounts[i].Id == id)
                {
                    index = i;
                    return accounts[i];
                }
            }
            index = -1;
            return null;
        }
        public void Put(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Account not find");
            account.Put(sum);
        }
        public void Withdraw(decimal sum, int id)
        {
            T account = FindAccount(id);
            if (account == null)
                throw new Exception("Account not find");
            account.Withdraw(sum);
        }
        public void Close(int id)
        {
            int index;
            T account = FindAccount(id, out index);
            if (account == null)
                throw new Exception("Account not find");
            account.Close();

            T[] tempAccounts = new T[accounts.Length - 1];
            for(int i = 0, j = 0; i < accounts.Length; ++i)
            {
                if (i != index)
                    tempAccounts[++j] = accounts[i];
            }
            accounts = tempAccounts;
        }
        public void CalculateP()
        {
            if (accounts == null)
                return;
            for(int i = 0; i < accounts.Length; ++i)
            {
                accounts[i].IncrementDay();
                accounts[i].Calculate();
            }
        }
    }
}
