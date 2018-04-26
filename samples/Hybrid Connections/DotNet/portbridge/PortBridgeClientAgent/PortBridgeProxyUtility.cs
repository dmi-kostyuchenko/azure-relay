using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PortBridgeClientAgent
{
    public static class PortBridgeProxyUtility
    {
        public static void EnableProxy()
        {
            var address = ConfigurationManager.AppSettings["ProxyAddress"];
            if (String.IsNullOrWhiteSpace(address)) return;

            ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Https;
            var proxy = new WebProxy(address);

            var userName = ConfigurationManager.AppSettings["ProxyUser"];
            var password = ConfigurationManager.AppSettings["ProxyPassword"];
            var userDomain = ConfigurationManager.AppSettings["ProxyUserDomain"];

            if (!String.IsNullOrWhiteSpace(userName) && !String.IsNullOrWhiteSpace(password))
            {
                proxy.Credentials = new NetworkCredential(userName, password, userDomain);
            }

            WebRequest.DefaultWebProxy = proxy;
        }
    }
}
