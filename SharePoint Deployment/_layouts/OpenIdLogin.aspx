<%@ Assembly Name="Microsoft.SharePoint.ApplicationPages, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@ Page Language="C#" MasterPageFile="~/_layouts/simple.master" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register Assembly="OrbitOne.OpenId.Controls" Namespace="OrbitOne.OpenId.Controls"  TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
    <SharePoint:EncodedLiteral ID="EncodedLiteral1" runat="server" text="<%$Resources:wss,login_pagetitle%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderId="PlaceHolderTitleBreadcrumb" runat="server">
&nbsp;
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderId="PlaceHolderPageTitleInTitleArea" runat="server">
    <SharePoint:EncodedLiteral ID="EncodedLiteral2" runat="server" text="<%$Resources:wss,login_pagetitle%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderId="PlaceHolderSiteName" runat="server"/>
<asp:Content ID="Content5" ContentPlaceHolderId="PlaceHolderMain" runat="server">
    
    
<style type="text/css">
.textImag
{
   
}
.textImag * input[type=text]
{
   background-position: 0 50%;
   padding-left:18px;
   border:1px solid black;
   background-color:white;
   background-image:url(openid-icon-small.gif);
   background-repeat:no-repeat;

}
</style>


    
    <fieldset style="text-align: center">
        <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
        <h2 class="none">
            &nbsp;</h2>
        <legend ><strong>Membership User Login</strong></legend>
 <asp:login id="login" FailureText="<%$Resources:wss,login_pageFailureText%>" runat=server width="100%">
    <layouttemplate>
        <asp:label id=FailureText class="ms-error" runat=server/>
        <table class="ms-input">
          <COLGROUP>
          <COL width=25%>
          <COL WIDTH=75%>
        <tr>
            <td noWrap><SharePoint:EncodedLiteral ID="EncodedLiteral3" runat="server" text="<%$Resources:wss,login_pageUserName%>" EncodeMethod='HtmlEncode'/></td>
            <td><asp:textbox id=UserName autocomplete="off" runat=server class="ms-long"/></td>
        </tr>
        <tr>
            <td noWrap><SharePoint:EncodedLiteral ID="EncodedLiteral4" runat="server" text="<%$Resources:wss,login_pagePassword%>" EncodeMethod='HtmlEncode'/></td>
            <td><asp:textbox id=password TextMode=Password autocomplete="off" runat=server class="ms-long"/></td>
        </tr>
        <tr>
            <td colSpan=2 align=right><asp:button id=login commandname="Login" text="<%$Resources:wss,login_pagetitle%>" runat=server /></td>
        </tr>
        <tr>
            <td colSpan=2><asp:CheckBox id=RememberMe text="<%$SPHtmlEncodedResources:wss,login_pageRememberMe%>" runat=server /></td>
        </tr>
        </table>
    </layouttemplate>
 </asp:login>
  </fieldset>
    <br />
  <fieldset >
        <legend><strong>OpenID User Login</strong></legend>&nbsp;<br /><br />
        
        <cc1:OpenIDLogin ID="Login1" UserNameLabelText="Open ID:" TitleText="" runat="server" LinkOpenIdPage="OpenIdUserLink.aspx" DestinationPageUrl="~/default.aspx" MembershipProvider="OpenIDMembershipProvider" CssClass="ms-input">
        </cc1:OpenIDLogin></fieldset>
</asp:Content>






