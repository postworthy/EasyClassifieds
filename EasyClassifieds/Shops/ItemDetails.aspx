<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ItemDetails.aspx.cs" Inherits="EasyClassifieds.Shops.ItemDetails" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/FormData.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.numbersOnly').keyup(function () {
                this.value = this.value.replace(/[^0-9\.]/g, '');
            });
            $('.numbersOnly').keydown(function (e) {
                var key = e.charCode || e.keyCode || 0;
                // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
                return (
                            key == 190 ||
                            key == 8 ||
                            key == 9 ||
                            key == 46 ||
                            (key >= 37 && key <= 40) ||
                            (key >= 48 && key <= 57) ||
                            (key >= 96 && key <= 105));
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>Listing Details</legend>
        <div>
            <label>
                Title:</label>
            <asp:TextBox ID="txtTitle" runat="server" />
            <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="* Required" ForeColor="red" ControlToValidate="txtTitle"/>
        </div>
        <div>
            <label>
                Description:</label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="600" Height="250" />
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ErrorMessage="* Required" ForeColor="red" ControlToValidate="txtDescription"/>
        </div>
        <div>
            <label></label><i>&lt;i&gt;</i> , <i>&lt;b&gt;</i> , <i>&lt;p&gt;</i> tags can be used for formatting text.
        </div>
        <div>
            <label>
                Serial Number:</label>
            <asp:TextBox ID="txtSerial" runat="server" />
        </div>
        <div>
            <label>
                Price:</label>
            <asp:TextBox ID="txtPrice" runat="server" CssClass="numbersOnly"/>
            <asp:RequiredFieldValidator ID="rfvPrice" runat="server" ErrorMessage="* Required" ForeColor="red" ControlToValidate="txtPrice" />
        </div>
        <div>
            <label>
                Category:</label>
            <asp:DropDownList ID="ddlCategory" runat="server" />
        </div>
        <div>
            <label>
                Image:</label>
            <asp:FileUpload ID="fuImage" runat="server" />
        </div>
        <div>
            <label>
            </label>
            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back to Listings" OnClientClick="window.location = 'manage.aspx';return false;" />
        </div>
        <fieldset runat="server" id="fsImages" visible="false">
            <legend>Images</legend>
            <asp:Repeater ID="rptImages" runat="server" 
                onitemcommand="rptImages_ItemCommand">
                <ItemTemplate>
                    <asp:LinkButton ID="lnk" runat="server" CommandName="DeleteImage" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure you want to delete this image?');" ToolTip="Click to Delete">
                        <img alt="Image" src="../image.ashx?id=<%# Eval("ID") %>" />
                    </asp:LinkButton>&nbsp;
                </ItemTemplate>
            </asp:Repeater>
        </fieldset>
    </fieldset>
</asp:Content>
