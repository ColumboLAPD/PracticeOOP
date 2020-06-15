using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public interface IAccount // интерфейс для банковского счёта
    {
        void Put(decimal sum); //функция зачисления на счет
        decimal Withdraw(decimal sum); //функция снятия со счета
    }
}
