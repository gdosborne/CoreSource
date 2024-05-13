/* File="Extensions"
   Company="Compressor Controls Corporation"
   Copyright="Copyright© 2024 Compressor Controls Corporation.  All Rights Reserved
   Author="Greg Osborne"
   Date="4/25/2024" */

using System.Net.NetworkInformation;

namespace OzFramework.Net {
    public static class Extensions {
        public static bool IsServerPortAvailable(string server, int port) {
            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
            var result = true;
            foreach (var tcpi in tcpConnInfoArray) {
                if (tcpi.LocalEndPoint.Port == port) {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
