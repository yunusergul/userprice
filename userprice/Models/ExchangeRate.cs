using System;
using System.Collections.Generic;

namespace userprice.Models
{
    public partial class ExchangeRate
    {
        public ExchangeRate()
        {
            MoneyTransactionsBalanceGr = new HashSet<MoneyTransactions>();
            MoneyTransactionsBalenceTip = new HashSet<MoneyTransactions>();
        }

        public int ExchangeRateId { get; set; }
        public double? RateP { get; set; }

        public virtual ExchangeRateName ExchangeRateName { get; set; }
        public virtual ICollection<MoneyTransactions> MoneyTransactionsBalanceGr { get; set; }
        public virtual ICollection<MoneyTransactions> MoneyTransactionsBalenceTip { get; set; }
    }
}
