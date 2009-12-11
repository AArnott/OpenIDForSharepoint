<%@ Page Language="C#" MasterPageFile="~/OpenId/MasterPage.master" AutoEventWireup="true"  Title="Specific User" %>

<%@ Register Src="../Controls/Specific_User.ascx" TagName="Specific_User" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="maincontent" Runat="Server">
    <uc1:Specific_User ID="Specific_User1" runat="server" />
</asp:Content>

