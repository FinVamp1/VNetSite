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
        protected void Page_Load(object sender, EventArgs e)
        {
            var netUtils = new NetworkUtils.NetworkItems();
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
                var netUtils = new NetworkUtils.NetworkItems();
                var dnsEntries = netUtils.DnsLookup(dnsHostName.Text);
                dnsResults.DataSource = dnsEntries;
                dnsResults.DataBind();
            }
        }
    }
}