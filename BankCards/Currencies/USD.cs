using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCards
{
    internal class USD : Currency
    {
        public USD(decimal amount) : base(amount) { }
        public USD() : base() { }

        public override decimal ToUAH()
        {
            return Amount * (decimal)29.9;
        }

        public override decimal FromUAH(decimal amount)
        {
            return amount / (decimal)29.9;
        }

        public override string ToString()
        {
            return "USD";
        }
    }
}
