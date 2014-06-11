<%@ Page Title="Shop" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Shop.aspx.cs" Inherits="Pages.Pages_Shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="lblOrders" runat="server"></asp:Label>
    <br />
    <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="Ok" 
        Visible="False" Width="100px" />
    <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
        Text="Cancel" Visible="False" Width="100px" />
    <br />
    <asp:Button ID="btnOrder" runat="server" onclick="btnOrder_Click" 
        Text="Order!" />
    <br />
    <asp:Label ID="lblError" runat="server"></asp:Label>
    <asp:Panel ID="pnlProducts" runat="server" CssClass=".pnlProducts">
    </asp:Panel>
    <br />

</asp:Content>

