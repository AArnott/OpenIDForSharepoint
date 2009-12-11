<%@ Page Language="C#" MasterPageFile="~/OpenId/MasterPage.master" AutoEventWireup="true" Title="Create User" %>

<%@ Register Src="../Controls/Create_User.ascx" TagName="Create_User" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
    <uc1:Create_User ID="Create_User" runat="server" />
</asp:Content>

