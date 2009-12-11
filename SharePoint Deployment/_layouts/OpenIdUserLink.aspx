<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%> 
<%@ Page Language="C#" MasterPageFile="~/_layouts/simple.master"%>
<%@ Register Src="OpenId_User_Link.ascx" TagName="OpenID_User_Link" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderId="PlaceHolderTitleBreadcrumb" runat="server">
&nbsp;
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
   Link OpenId with user account
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderId="PlaceHolderSiteName" runat="server"/>
<asp:Content ID="Content5" ContentPlaceHolderId="PlaceHolderMain" runat="server">
      <uc1:OpenID_User_Link ID="OpenID_User_Link" runat="server" OpenIdMembershipProvider="OpenIDMembershipProvider" />    
</asp:Content>