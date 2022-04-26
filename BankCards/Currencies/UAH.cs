using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCards
{
    internal class UAH : Currency
    {
        public UAH(decimal amount) : base(amount) { }
        public UAH() : base() { }

        public override decimal ToUAH()
        {
            return Amount;
        }

        public override decimal FromUAH(decimal amount)
        {
            return amount;
        }
        public override string ToString()
        {
            return "UAH";
        }
    }
}
