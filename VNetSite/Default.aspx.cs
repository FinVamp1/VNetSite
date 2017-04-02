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
    }
}