<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ShopDetails.aspx.cs" Inherits="EasyClassifieds.ShopDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/Listing.css" rel="stylesheet" type="text/css" />
    <link href="Styles/FormData.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend><%= Entity.Name %> Details</legend>
        <%if (ShopImageID != "0"){%>
        <img src="image.ashx?shopimageid=<%= ShopImageID %>" alt="<%= Entity.Name %>" style="float:right;"/>
        <%} %>
        <div>
            <label>
                Address:</label>
            <asp:Literal ID="litAddress" runat="server" />
        </div>
        <%--<div>
            <label>
                Email:</label>
            <asp:Literal ID="litEmail" runat="server" />
        </div>--%>
        <div>
            <label>
                Phone Number:</label>
            <asp:Literal ID="litPhone" runat="server" />
        </div>
    </fieldset>
    <fieldset>
        <legend>
            <%= Entity.Name %> Listings</legend>
        <asp:Repeater ID="listings" runat="server">
            <ItemTemplate>
                <a href="itemdetails.aspx?id=<%# Eval("ID") %>"><b><%# Server.HtmlEncode(Eval("Title").ToString()) %></b></a> (Price: <b>$<%# Eval("Price") %></b>)<br />
            </ItemTemplate>
        </asp:Repeater>
    </fieldset>
</asp:Content>
