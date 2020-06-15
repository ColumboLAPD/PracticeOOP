using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        //объявление событий для соответствующих действий
        protected internal event AccountStateHandler WithDraw;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        public decimal Sum { get; set; }
        public decimal Percent { get; set; }
        public int Id { get; set; }

        static int counter = 0;//счетчик для id аккаунтов

        public Account(decimal _sum, decimal _percent)
        {
            Sum = _sum;
            Percent = _percent;
            Id = ++counter;
        }
        //функция для вызова события
        private void CallEvent(AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }
        //события для операций
        protected virtual void OnWithDraw(AccountEventArgs e)
        {
            CallEvent(e, WithDraw);
        }
        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnOpened(AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculate(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArgs("To the account received " + sum, sum));
        }
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = Sum;
                OnWithDraw(new AccountEventArgs($"Sum {sum} Withdraw from account {Id} ", sum));
            }
            else
                OnWithDraw(new AccountEventArgs($"Not enough money in the account {Id}", 0));

            return result;
        }
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Open new account! ID: {Id}", Sum));
        }
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Account {Id} closed. Total sum: {Sum}", Sum));
        }
        protected  internal virtual void Calculate()
        {
            decimal increment = Sum * Percent / 100;
            Sum += increment;
            OnCalculate(new AccountEventArgs($"Interest accrued: {increment}", increment));
        }

        protected int _days  = 0;
        protected internal void IncrementDay()
        {
            ++_days;
        }
    }
}
