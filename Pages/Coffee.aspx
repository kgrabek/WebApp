<%@ Page Title="Coffee Reviews" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Coffee.aspx.cs" Inherits="Pages.Pages_Coffee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
    Select a type:
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        DataSourceID="sds_type" DataTextField="type" DataValueField="type" 
        onselectedindexchanged="DropDownList1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:SqlDataSource ID="sds_type" runat="server" 
        ConnectionString="<%$ ConnectionStrings:g3f4.CoffeeDB.dbo %>" 
        SelectCommand="SELECT DISTINCT [type] FROM [coffee] ORDER BY [type]">
    </asp:SqlDataSource>
</p>
<p>
    <asp:Label ID="lblOuput" runat="server" Text="Label"></asp:Label>
</p>
</asp:Content>

