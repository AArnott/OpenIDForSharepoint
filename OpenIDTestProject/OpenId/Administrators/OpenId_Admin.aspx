<%@ Page Language="C#" MasterPageFile="~/OpenId/MasterPage.master" AutoEventWireup="true"  %>

<%@ Register Src="../Controls/OpenId_Admin.ascx" TagName="OpenId_Admin" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
   <uc1:OpenId_Admin ID="OpenId_Admin2" runat="server" />
</asp:Content>