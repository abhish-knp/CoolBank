using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolBank
{
    public class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public String Notes { get; }

        public Transaction(decimal amount, DateTime date, string notes)
        {
            this.Amount = amount;
            this.Notes = notes;
            this.Date = date;

        }
    }
}
