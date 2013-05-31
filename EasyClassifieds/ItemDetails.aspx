<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ItemDetails.aspx.cs" Inherits="EasyClassifieds.ItemDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="Styles/FormData.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Listing Details</legend>
        <div runat="server" id="isFeatured" visible="false">
            <label>
                Is Featured:</label>
            <asp:CheckBox ID="chkIsFeatured" runat="server" AutoPostBack="true" OnCheckedChanged="chkIsFeatured_CheckedChanged" />
            <asp:Literal ID="litNoImages" runat="server" Visible="false">* Can not feature an item with no images!</asp:Literal>
        </div>
        <div>
            <label>
                Title:</label>
            <asp:Literal ID="litTitle" runat="server" />
        </div>
        <div>
            <label>
                Description:</label>
                <div style="display:inline-block;width:600px;">
                    <asp:Literal ID="litDescription" runat="server" />
                </div>
        </div>
        <div>
            <label>
                Price:</label>
            $<asp:Literal ID="litPrice" runat="server" />
        </div>
        <div>
            <label>
                Category:</label>
            <asp:Literal ID="litCategory" runat="server" />
        </div>
        <div>
            <label>
                Shop:</label>
            <asp:Literal ID="litShop" runat="server" />
        </div>
        <div>
            <label>
                Contact Info:</label>
            <asp:Literal ID="litPhone" runat="server" />
        </div>
        <div>
            <label>
                Views:</label>
            <asp:Literal ID="litViews" runat="server" />
        </div>
    </fieldset>
    <fieldset id="fsImages" runat="server" visible="false">
        <legend>Images</legend>
        <asp:Repeater ID="rptImages" runat="server">
            <ItemTemplate>
                <img src="../image.ashx?id=<%# Eval("ID") %>" />&nbsp;
            </ItemTemplate>
        </asp:Repeater>
    </fieldset>
</asp:Content>
