using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankCards
{
    internal abstract class Currency
    {
        public decimal Amount { get; set; }
        public Currency(decimal amount) => Amount = amount;
        public Currency() : this(0) { }

        public abstract decimal ToUAH();
        public abstract decimal FromUAH(decimal amount);
    }
}
