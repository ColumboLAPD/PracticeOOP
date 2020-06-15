using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    //делегат, инициализирующий события изменения счёта
    public delegate void AccountStateHandler(object sender, AccountEventArgs e);
    public class AccountEventArgs
    {
        public string Message { get; private set; } //сообщение об изменении состояния
        public decimal Sum { get; private set; } //сумма, на которую изменится счёт
        public AccountEventArgs(string _mes, decimal _sum) 
        {
            Message = _mes;
            Sum = _sum;
        }
    }
}
