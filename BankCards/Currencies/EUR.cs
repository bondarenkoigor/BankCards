using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCards
{
    internal class EUR : Currency
    {
        public EUR(decimal amount) : base(amount) { }
        public EUR() : base() { }

        public override decimal ToUAH()
        {
            return Amount * 32;
        }

        public override decimal FromUAH(decimal amount)
        {
            return amount / 32;
        }

        public override string ToString()
        {
            return "EUR";
        }
    }
}
