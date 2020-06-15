using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DemandAccount : Account
    {
        public DemandAccount(decimal _sum, decimal _percent)
            : base(_sum, _percent)
        {

        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Open demand account! ID: {this.Id}", this.Sum));
        }
    }
}
