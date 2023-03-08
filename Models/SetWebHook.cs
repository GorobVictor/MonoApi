using System;
using System.Collections.Generic;
using System.Text;

namespace MonoApi.Models
{
    public class SetWebHook
    {
        public SetWebHook()
        {
        }

        public SetWebHook(string webHookUrl)
        {
            this.WebHookUrl = webHookUrl;
        }

        public string WebHookUrl { get; set; }
    }
}
