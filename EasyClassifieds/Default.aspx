<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Listing.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="EasyClassifieds._Default" %>

<asp:Content ID="ListingContent" runat="server" ContentPlaceHolderID="Listings">
    <asp:Repeater ID="listings" runat="server">
        <ItemTemplate>
            <a href="itemdetails.aspx?id=<%# Eval("ID") %>"><b><%# Server.HtmlEncode(Eval("Title").ToString()) %></b></a> (Price: <b>$<%# Eval("Price") %></b>) (Shop: <a href="shopdetails.aspx?id=<%# Eval("Shop.ID") %>"><%# Eval("Shop.Name") %></a>)<br />
        </ItemTemplate>
    </asp:Repeater>
    <br />
    <% if (CurrentPageIndex > 0) { %><a href="?page=<%= CurrentPageIndex.ToString() + QueryUrl %>">&lt;&lt; Prev <%= PageSize %></a><%} %> <%= " " + (CurrentPageIndex + 1) + " of " + PageCount %> <% if (PageCount > 1 && CurrentPageIndex < PageCount - 1) { %><a href="?page=<%= (CurrentPageIndex + 2).ToString() + QueryUrl %>">Next <%= PageSize %> &gt;&gt;</a><%} %>
</asp:Content>
