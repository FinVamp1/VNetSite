<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VNetSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">Networking Site Extension </div>
            <div class="panel-body">
                <p>This Site Extension assists in diagnosing Azure App Service Virtual Networking issues. </p>

                    <div class="row">                 
                            <div class="col-md-2">
                            <p class="h4 text-primary">IP Addresses</p>
                                <asp:DataGrid id="NicGrid" runat="server" AlternatingItemStyle-BackColor="Teal" gridlines="None">
                                <HeaderStyle Font-Bold="True" Font-Underline="true" />
                                </asp:DataGrid>
                            </div>
                            <div class="col-md-2">
                                <p class="h4 text-primary">DNS Servers</p>
                                <asp:DataGrid id="DNSGrid" runat="server" gridlines="None">
                                <HeaderStyle Font-Bold="True" Font-Underline="true"  />
                                </asp:DataGrid>
                            </div>
                    </div>    
                    <div class="row">
                             <div class="col-md-2">
                                <p class="h4 text-primary">DNS Lookup</p>
                                <asp:Label runat="server"> DNS Hostname</asp:Label><asp:TextBox runat="server" ID="dnsHostName" Text="Enter DNS Hostname"></asp:TextBox>
                                <asp:Label runat="server"> DNS Results</asp:Label>
                                <asp:ListBox runat="server" ID="dnsResults" BorderColour="SeaGreen">
                                     <asp:ListItem>DNS Entries will be here</asp:ListItem>
                                 </asp:ListBox><br>
                                 <asp:Button runat="server" ID="Lookup" CausesValidation="true" OnClick="Lookup_Click" Text ="Lookup" CssClass="btn btn-primary" />
                            </div>
                            <div class="col-md-2">
                                <p class="h4 test-primary">Route Lookup</p>
                                <asp:Label runat="server"> Remote IP</asp:Label><asp:TextBox runat="server" ID="remoteIP" Text="Enter Remote IP "></asp:TextBox>
                                <asp:ListBox runat="server" ID="IPEndPoint" BorderColour="SeaGreen">
                                     <asp:ListItem>Route Endpoint is here</asp:ListItem>
                                 </asp:ListBox><br>
                                <asp:Button runat="server" ID="RouteSrch" CausesValidation="true" OnClick="RouteSrch_Click" Text ="Search" CssClass="btn btn-primary" />
                            </div>
                    </div>                
          </div>
          <div class="panel-footer">
              <p><a href="https://docs.microsoft.com/en-us/azure/app-service-web/web-sites-integrate-with-vnet" class="btn btn-info" target="_blank">Learn more about Networking &raquo;</a></p>
          </div>
    </div>



</asp:Content>
