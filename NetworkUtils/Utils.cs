using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

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
        public IPEndPoint RouteLookup(Socket socket, IPEndPoint remoteEndPoint)
        {
            SocketAddress address = remoteEndPoint.Serialize();

            byte[] remoteAddrBytes = new byte[address.Size];
            for (int i = 0; i < address.Size; i++)
            {
                remoteAddrBytes[i] = address[i];
            }

            byte[] outBytes = new byte[remoteAddrBytes.Length];
            socket.IOControl(
                        IOControlCode.RoutingInterfaceQuery,
                        remoteAddrBytes,
                        outBytes);
            for (int i = 0; i < address.Size; i++)
            {
                address[i] = outBytes[i];
            }

            EndPoint ep = remoteEndPoint.Create(address);
            return (IPEndPoint)ep;
        }
        public IPEndPoint GetRemoteEndPoint(string remoteIPstr)
        {
            IPAddress remoteIP = IPAddress.Parse(remoteIPstr);
            IPEndPoint remoteEndpoint = new IPEndPoint(remoteIP, 0);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint localEndPoint = RouteLookup(socket, remoteEndpoint);
            return localEndPoint;
        }
    }
}
