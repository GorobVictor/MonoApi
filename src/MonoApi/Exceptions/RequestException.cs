using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MonoApi.Exceptions
{
    public class RequestException : Exception
    {
        public RequestException(HttpStatusCode code, string message) : base(message)
        {
            this.StatusCode = code;
        }

        public HttpStatusCode StatusCode;
    }
}
