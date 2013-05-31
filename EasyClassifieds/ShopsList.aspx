<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ShopsList.aspx.cs" Inherits="EasyClassifieds.ShopsList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="Styles/Listing.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" id="lists" runat="server">
        <tr>
            <td id="col0" runat="server" valign="top"></td>
            <td id="col1" runat="server" valign="top"></td>
            <td id="col2" runat="server" valign="top"></td>
            <td id="col3" runat="server" valign="top"></td>
        </tr>
    </table>
</asp:Content>
