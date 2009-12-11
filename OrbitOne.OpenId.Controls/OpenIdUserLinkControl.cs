using System;
using System.Web.Security;
using System.Web.UI.WebControls;
using DotNetOpenAuth.OpenId;
using OrbitOne.OpenId.MembershipProvider;
namespace OrbitOne.OpenId.Controls
{
    public  class OpenIdUserLinkControl : System.Web.UI.UserControl
    {
        private static readonly string HEADTEXT =
            "The OpenID: <b>{0}</b> you are trying to login with is not linked with any site user, please link this OpenID with your site user account:";

        protected Login LinkOpenIdLogin;
        protected Label headLabel;

        public Identifier OpenId { get; set; }

        [System.ComponentModel.Browsable(true),
             System.ComponentModel.Description("OpenId MembershipProvider name")]
        public string OpenIdMembershipProvider { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            OpenId = Page.Request.QueryString["openId"];
            headLabel.Text = string.Format(HEADTEXT, OpenId);
        }

        protected void LinkOpenIdLogin_LoggedIn(object sender, EventArgs e)
        {
            OpenIdMembershipProvider provider = (OpenIdMembershipProvider)Membership.Providers[OpenIdMembershipProvider];
            MembershipUser user = Membership.Providers[LinkOpenIdLogin.MembershipProvider].GetUser(LinkOpenIdLogin.UserName, false);
            provider.LinkUserWithOpenId(OpenId, user.ProviderUserKey);
            FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
        }
    }
}