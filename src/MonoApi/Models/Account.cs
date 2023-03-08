using System;
using System.Collections.Generic;
using System.Text;

namespace MonoApi.Models
{
    public class Account
    {
        public string Id { get; set; }
        public string SendId { get; set; }
        public long Balance { get; set; }
        public long CreditLimit { get; set; }
        public string Type { get; set; }
        public long CurrencyCode { get; set; }
        public string CashbackType { get; set; }
        public List<string> MaskedPan { get; set; }
        public string Iban { get; set; }
    }
}
