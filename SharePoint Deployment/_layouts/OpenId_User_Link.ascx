<%@ Control Language="C#" AutoEventWireup="true" Inherits="OrbitOne.OpenId.Controls.OpenIdUserLinkControl" %>
 <fieldset style="text-align: center">
        <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
       
     <asp:Label ID="headLabel" runat="server" Text="Label"></asp:Label>
        <legend ><strong>Input Membership User Login</strong></legend>
<asp:Login ID="LinkOpenIdLogin" RememberMeSet="false" DisplayRememberMe="False" LoginButtonText="Link" runat="server" OnLoggedIn="LinkOpenIdLogin_LoggedIn" MembershipProvider="SqlMembershipProvider" DestinationPageUrl="~/default.aspx" >
</asp:Login>
    </fieldset>