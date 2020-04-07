using System;
using System.Collections.Generic;

namespace userprice.Models
{
    public partial class Operation
    {
        public Operation()
        {
            MoneyTransactions = new HashSet<MoneyTransactions>();
        }

        public int OperationId { get; set; }
        public string OperationName { get; set; }

        public virtual ICollection<MoneyTransactions> MoneyTransactions { get; set; }
    }
}
