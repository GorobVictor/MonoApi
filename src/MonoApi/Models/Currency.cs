using System;
using System.Collections.Generic;
using System.Text;

namespace MonoApi.Models
{
    public class Currency
    {
        public long currencyCodeA { get; set; }
        public long currencyCodeB { get; set; }
        public long date { get; set; }
        public long rateSell { get; set; }
        public float rateBuy { get; set; }
        public float rateCross { get; set; }
    }
}
