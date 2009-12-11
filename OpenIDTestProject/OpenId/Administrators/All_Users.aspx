<%@ Page Language="C#" MasterPageFile="~/OpenId/MasterPage.master" AutoEventWireup="true" %>

<%@ Register Src="../Controls/All_Users.ascx" TagName="All_Users" TagPrefix="uc1" %>
<asp:Content ID="Content" ContentPlaceHolderID="maincontent" Runat="Server">
    <uc1:All_Users ID="All_Users1" runat="server" />
</asp:Content>

