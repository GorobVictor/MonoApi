using System;
using System.Collections.Generic;
using System.Text;

namespace MonoApi.Models
{
    public class Extract
    {
        public string Id { get; set; }
        public long Time { get; set; }
        public string Description { get; set; }
        public long Mcc { get; set; }
        public long OriginalMcc { get; set; }
        public bool Hold { get; set; }
        public long Amount { get; set; }
        public long OperationAmount { get; set; }
        public long CurrencyCode { get; set; }
        public long CommissionRate { get; set; }
        public long CashbackAmount { get; set; }
        public long Balance { get; set; }
        public string Comment { get; set; }
        public string ReceiptId { get; set; }
        public string InvoiceId { get; set; }
        public string CounterEdrpou { get; set; }
        public string CounterIban { get; set; }
        public string CounterName { get; set; }
    }
}
