<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="VNetSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="panel panel-primary">
        <div class="panel-heading">Networking Site Extension </div>
            <div class="panel-body">
                <p>This Site Extension assists in diagnosing Azure App Service Virtual Networking issues. </p>

                    <div class="row">                 
                            <div class="col-md-2">
                            <p class="h4 text-primary">Ip Addresses</p>
                                <asp:DataGrid id="NicGrid" runat="server" AlternatingItemStyle-BackColor="Teal" gridlines="None">
                                <HeaderStyle Font-Bold="True" Font-Underline="true" />
                                </asp:DataGrid>
                            </div>
                            <div class="col-md-2">
                                <p class="h4 text-primary">DNS Servers</p>
                                <asp:DataGrid id="DNSGrid" runat="server" AlternatingItemStyle-BackColor="Teal" GridLines="None">
                                <HeaderStyle Font-Bold="True" Font-Underline="true" />
                                </asp:DataGrid>
                            </div>
                    </div>                    
          </div>
          <div class="panel-footer">
              <p><a href="https://docs.microsoft.com/en-us/azure/app-service-web/web-sites-integrate-with-vnet" class="btn btn-info">Learn more &raquo;</a></p>
          </div>
    </div>



</asp:Content>
