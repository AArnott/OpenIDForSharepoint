using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using OrbitOne.OpenId.MembershipProvider;

public partial class OpenId_Controls_OpenId_Admin : System.Web.UI.UserControl
{
    protected OpenIdMembershipProvider OpenIdMembershipProvider
    {
        get
        {
            return (OpenIdMembershipProvider)Membership.Providers["OpenIDMembershipProvider"];
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rpt_membership_user_BindData();
        }
    }

    private void rpt_membership_user_BindData()
    {
        int totalCount;
        MembershipUserCollection users = OpenIdMembershipProvider.GetAllUsers(0, 10, out totalCount);
        rpt_membership_user.DataSource = users;
        rpt_membership_user.DataBind();
    }


    protected void rpt_membership_user_DataBound(object sender, RepeaterItemEventArgs e)
    {
       
        
        
        Repeater rptOpenId = e.Item.FindControl("rpt_openId") as Repeater;
       
        if (rptOpenId != null && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {

           


            string userName = ((MembershipUser) e.Item.DataItem).UserName;
            rptOpenId.DataSource = GetOpenIdsByUserName(userName);
            rptOpenId.DataBind();
        }
    }
    
    private IList<string> GetOpenIdsByUserName(string userName)
    {
        return OpenIdMembershipProvider.GetOpenIdsByUserName(userName);
    }

   

    protected void rpt_membership_user_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "AddNewOpenId" && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
       {
           TextBox tbxNewOpenId = e.Item.FindControl("tbxNewOpenId") as TextBox;
           HtmlInputHidden inputHiddenUserId = e.Item.FindControl("hiddenInputUserId") as HtmlInputHidden;
           if (!string.IsNullOrEmpty(tbxNewOpenId.Text.Trim()))
           {
               OpenIdMembershipProvider.LinkUserWithOpenId(tbxNewOpenId.Text, inputHiddenUserId.Value);
               rpt_membership_user_BindData();
           } 
       }
    }

    protected void rpt_openId_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "RemoveOpenId" && (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem))
        {
            HtmlInputHidden inputHiddenOpenId = e.Item.FindControl("hiddenInputOpenId") as HtmlInputHidden;


            OpenIdMembershipProvider.RemoveUserOpenIdLinkByOpenId(inputHiddenOpenId.Value);
            rpt_membership_user_BindData();
        }
    }
}
