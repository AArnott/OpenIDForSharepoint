<%@ Page Language="C#" MasterPageFile="~/OpenId/MasterPage.master" AutoEventWireup="true" %>

<%@ Register Src="../Controls/Permissions_And_Roles.ascx" TagName="Permissions_And_Roles"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
    <uc1:Permissions_And_Roles ID="Permissions_And_Roles1" runat="server" />
</asp:Content>

