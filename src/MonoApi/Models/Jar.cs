using System;
using System.Collections.Generic;
using System.Text;

namespace MonoApi.Models
{
    public class Jar
    {
        public string Id { get; set; }
        public string SendId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CurrencyCode { get; set; }
        public long Balance { get; set; }
        public long? Goal { get; set; }
    }
}
