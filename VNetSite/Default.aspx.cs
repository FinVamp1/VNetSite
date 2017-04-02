using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetworkUtils;

namespace VNetSite
{
    public partial class _Default : Page
    {
        static NetworkItems netUtils = new NetworkUtils.NetworkItems();
        protected void Page_Init(object sender, EventArgs e)
        {
 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
             var activeNics = netUtils.GetNetworkCards();
            var ipAddresses = netUtils.GetIpAddresses(activeNics);
            var dnsServers = netUtils.GetDNSServers(activeNics);
            NicGrid.DataSource = ipAddresses;
            NicGrid.DataBind();
            DNSGrid.DataSource = dnsServers;
            DNSGrid.DataBind();
        }

        protected void Lookup_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(dnsHostName.Text))
            {
                dnsResults.Items.Clear();
                dnsResults.Items.Add(new ListItem("Please enter a hostname"));
            }
            else
            {
                dnsResults.Items.Clear();
                var dnsEntries = netUtils.DnsLookup(dnsHostName.Text);
                dnsResults.DataSource = dnsEntries;
                dnsResults.DataBind();
            }
        }
        protected void RouteSrch_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(remoteIP.Text))
            {
                IPEndPoint.Items.Clear();
                IPEndPoint.Items.Add(new ListItem("Please Enter an IP Address"));
            }
            else
            {
                IPEndPoint.Items.Clear();
                var remoteEndpoints = netUtils.GetRemoteEndPoint(remoteIP.Text);
                IPEndPoint.Items.Add(new ListItem(remoteEndpoints.Address.ToString()));
            }
        }


    }
}