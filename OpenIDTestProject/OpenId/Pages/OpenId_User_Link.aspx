<%@ Page Language="C#"  MasterPageFile="~/OpenId/MasterPage.master"  AutoEventWireup="true" Title="Link OpenId to User Account" %>

<%@ Register Src="../Controls/OpenId_User_Link.ascx" TagName="OpenID_User_Link" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
    <uc1:OpenID_User_Link ID="OpenID_User_Link" runat="server" OpenIdMembershipProvider="OpenIDMembershipProvider" />    
</asp:Content>

