<%@ Page Language="C#" MasterPageFile="~/OpenId/MasterPage.master" AutoEventWireup="true"  Title="Login" %>

<%@ Register Src="../Controls/Login_OpenID.ascx" TagName="Login_OpenID" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
    <uc1:Login_OpenID ID="Login_OpenID1" runat="server" />   
</asp:Content>

