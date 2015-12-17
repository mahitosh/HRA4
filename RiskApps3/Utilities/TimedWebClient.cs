using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace RiskApps3.Utilities
{
    public class TimedWebClient : WebClient
    {
        /// <summary>
        /// Time in milliseconds
        /// </summary>
        public int Timeout { get; set; }

        public TimedWebClient() : this(60000) { }

        public TimedWebClient(int timeout)
        {
            this.Timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request != null)
            {
                request.Timeout = this.Timeout;
            }
            return request;
        }
    }
}
