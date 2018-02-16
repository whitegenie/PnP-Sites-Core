using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.SharePoint.Client
{
    public static class ContextUserAgent
    {
        public static void AddUserAgent(this ClientContext ctx) {
            ctx.ExecutingWebRequest += delegate (object sender, WebRequestEventArgs e)
            {
                e.WebRequestExecutor.WebRequest.UserAgent = "NONISV|Ferrero|Forward/1.0";
            };
        }
    }
}
