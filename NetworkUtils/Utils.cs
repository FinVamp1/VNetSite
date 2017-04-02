using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NetworkUtils
{
    public class NetworkItems
    {
        public IEnumerable<NetworkInterface> GetNetworkCards()
        {
            var ipAddresses = new List<string>();
            var activeNics =
                (from networkInterface in NetworkInterface.GetAllNetworkInterfaces()
                 let statistics = networkInterface.GetIPv4Statistics()
                 where
                    networkInterface.OperationalStatus == OperationalStatus.Up
                    && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel
                    && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback
                    && (statistics.BytesReceived > 0) && (statistics.BytesSent > 0)
                 select networkInterface);
            return activeNics;
        }

        public List<string> GetIpAddresses(IEnumerable<NetworkInterface> myNics)
        {
            var activeNics = myNics;
            var ipInfo = new List<string>();
            foreach (NetworkInterface nic in activeNics)
            {
                var ips = nic.GetIPProperties().UnicastAddresses;
                foreach (var ip in ips)
                {
                    ipInfo.Add(ip.Address.ToString());
                }
            }

            return ipInfo;
        }

        public List<string> GetDNSServers(IEnumerable<NetworkInterface> myNics)
        {
            var activeNics = myNics;
            var dnsInfo = new List<string>();
            foreach (NetworkInterface nic in myNics)
            {
                var dnsServers = nic.GetIPProperties().DnsAddresses;
                foreach (var dnsServer in dnsServers)
                {
                    dnsInfo.Add(dnsServer.ToString());
                }
            }
            return dnsInfo;
        }

        public List<string> DnsLookup(string hostName)
        {
            var ipList = new List<string>();
            try
            {
                IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
                IPAddress[] ipAddrs = hostEntry.AddressList;
                foreach (IPAddress ip in ipAddrs)
                {
                    ipList.Add(ip.ToString());
                }


            }
            catch (Exception ex)
            {
                ipList.Add(ex.Message.ToString());
            }
            return ipList;
        }
    }
}
