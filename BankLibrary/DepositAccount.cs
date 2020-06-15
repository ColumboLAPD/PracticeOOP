using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DepositAccount : Account
    {
        public DepositAccount(decimal _sum, decimal _percent)
            : base(_sum, _percent)
        {

        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Open deposit account! ID: {this.Id}", this.Sum));
        }
        public override void Put(decimal sum)
        {
            if (_days > 30)
                base.Put(sum);
            else
                base.OnAdded(new AccountEventArgs("Added not possible! 30 days must pass!", 0));
        }
        public override decimal Withdraw(decimal sum)
        {
            if (_days > 30)
                return base.Withdraw(sum);
            else
                base.OnWithDraw(new AccountEventArgs($"Withdraw not possible! 30 days must pass1", 0));
            return 0;
        }
    }
}
