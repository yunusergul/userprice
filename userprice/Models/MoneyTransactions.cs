using System;
using System.Collections.Generic;

namespace userprice.Models
{
    public partial class MoneyTransactions
    {
        public int MoneyTransactionsId { get; set; }
        public int UserId { get; set; }
        public int AmountMoney { get; set; }
        public int BalenceTipId { get; set; }
        public int BalanceGrId { get; set; }
        public int OpaoperationId { get; set; }
        public int BalanceCont { get; set; }
        public DateTime? TrDate { get; set; }

        public virtual ExchangeRate BalanceGr { get; set; }
        public virtual ExchangeRate BalenceTip { get; set; }
        public virtual Operation Opaoperation { get; set; }
        public virtual User User { get; set; }
    }
}
