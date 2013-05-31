<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Manage.aspx.cs" Inherits="EasyClassifieds.Shops.Manage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/FormData.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            <%= Entity.Name %>
            Details</legend>
        <%if (ShopImageID != "0"){%>
        <asp:ImageButton ID="imgShop" runat="server" OnClick="DeleteShopImage" OnClientClick="return confirm('Are you sure you want to delete this image?');" ToolTip="Click to Delete" style="float:right;" />
        <%} %>
        <div>
            <label>
                Account:</label>
            <a id="A1" href="~/Account/ChangePassword.aspx" runat="server">Change Password</a>
        </div>
        <div>&nbsp;</div>
        <div>
            <label>
                Title:</label>
            <asp:TextBox ID="txtName" runat="server" Width="200px" />
        </div>
        <div>
            <label>
                Phone Number:</label>
            <asp:TextBox ID="txtPhone" runat="server" Width="100"/>
        </div>
        <div>
            <label>
                Address:</label>
            <asp:TextBox ID="txtAddress" runat="server" Width="400px" TextMode="MultiLine" />
        </div>
        <div>
            <label>
                Zip Code:</label>
            <asp:TextBox ID="txtZip" runat="server" Width="75"/>
        </div>
        <div>
            <label>
                Image:</label>
            <asp:FileUpload ID="fuImage" runat="server" />
        </div>
        <div>
            <label>
            </label>
            <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" OnClick="btnSaveDetails_Click" />
        </div>
    </fieldset>
    <fieldset>
        <legend>
            <%= Entity.Name %>
            Listings (<asp:CheckBox ID="chkShowSold" runat="server" Text="Show Sold Items" AutoPostBack="true"/>)</legend>
        <asp:Button ID="btnNewListing" runat="server" Text="Create New Listing" OnClientClick="window.location = 'itemdetails.aspx';return false;" />
        <br />
        <br />
        <asp:Repeater ID="listings" runat="server" 
            onitemdatabound="listings_ItemDataBound">
            <ItemTemplate>
                <%# (bool)Eval("IsSold") ? "<del>" : "" %>
                <%# Eval("ID") %>) <a href="itemdetails.aspx?id=<%# Eval("ID") %>">
                    <b><%# Eval("Title") %></b></a> (Price:
                <b>$<%# Eval("Price") %></b>) (Views: <%# Eval("Views") %>) 
                <%# (bool)Eval("IsSold") ? "</del>" : "" %>
                <asp:LinkButton ID="btnSold" runat="server" OnClick="ToggleIsSold" /><br />
            </ItemTemplate>
        </asp:Repeater>
    </fieldset>
</asp:Content>
