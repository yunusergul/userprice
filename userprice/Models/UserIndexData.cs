
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using userprice.Models;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class UserIndexData
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<MoneyTransactions> MoneyTransactionss { get; set; }
        public IEnumerable<Operation> Operations { get; set; }
        public IEnumerable<ExchangeRate> ExchangeRates { get; set; }

    }
}