using System;
using System.Collections.Generic;

namespace userprice.Models
{
    public partial class User
    {
        public User()
        {
            MoneyTransactions = new HashSet<MoneyTransactions>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLastname { get; set; }
        public string UserPpNo { get; set; }
        public string UserPass { get; set; }

        public virtual ICollection<MoneyTransactions> MoneyTransactions { get; set; }
    }
}
