<%@ Control Language="C#" AutoEventWireup="true" %>
<%@ Register Assembly="OrbitOne.OpenId.Controls" Namespace="OrbitOne.OpenId.Controls"  TagPrefix="cc1" %>

    <fieldset style="text-align: center">
        <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
        <h2 class="none">
            &nbsp;</h2>
        <legend ><strong>Membership User Login</strong></legend>
<asp:Login ID="Login" runat="server" DestinationPageUrl="~/OpenId/Pages/About.aspx" MembershipProvider="SqlProvider">
</asp:Login>
    </fieldset>
    &nbsp;<br />
    <fieldset style="text-align: center">
        <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
        <h2 class="none">
            &nbsp;</h2>
        <legend>OpenID User Login</legend>&nbsp;<br />
        <br />
        &nbsp; &nbsp;<cc1:OpenIDLogin ID="Login1" runat="server" LinkOpenIdPage="OpenId_User_Link.aspx" DestinationPageUrl="~/OpenId/Pages/About.aspx" MembershipProvider="OpenIDMembershipProvider" CssClass="textImag">
        </cc1:OpenIDLogin></fieldset>
    &nbsp;
