using System;
using System.Collections.Generic;
using System.Text;

namespace MonoApi.Models
{
    public class Client
    {
        public string ClientId { get; set; }
        public string Name { get; set; }
        public string WebHookUrl { get; set; }
        public string Permissions { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Jar> Jars { get; set; }
    }
}
