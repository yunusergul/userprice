using System;
using System.Collections.Generic;

namespace userprice.Models
{
    public partial class ExchangeRateName
    {
        public int ExchangeRateNameId { get; set; }
        public string Name { get; set; }

        public virtual ExchangeRate ExchangeRateNameNavigation { get; set; }
    }
}
