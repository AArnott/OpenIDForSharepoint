using System;
using System.Web.UI.WebControls;
using OrbitOne.OpenId.TestProject;

public partial class Open_Id_Controls_Permissions_And_Roles : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

   
    #region Events
    protected void ListUserInRole_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete" && ListUser.SelectedValue != null)
        {
            DataProvider dp = new DataProvider();
            string roleName = e.CommandArgument.ToString();
            dp.DeleteUsersFromRoles(ListUser.SelectedValue.ToString(), roleName);
        }               
    }

    protected void btnAddRole_Click(object sender, EventArgs e)
    {
        if (txtRoleName.Text.CompareTo(String.Empty) != 0)
        {
          
                DataProvider dp = new DataProvider();
                dp.AddRoles(txtRoleName.Text);

                RolesData.Select();
                ListAllRoles.DataBind();
            
        }
    }

    protected void ListAllRoles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select" && ListUser.SelectedValue != null)
        {
            DataProvider dp = new DataProvider();
            string roleName = e.CommandArgument.ToString();
            dp.AddUsersToRoles(ListUser.SelectedValue.ToString(), roleName);

            UserInRoleData.Select();
            ListUserInRole.DataBind();
        }     
    }

   
    protected void ListUserInRole_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListUserInRole.PageIndex = e.NewPageIndex;
        ListUserInRole.DataBind();
    }

 
    protected void ListAllRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ListAllRoles.PageIndex = e.NewPageIndex;
        ListAllRoles.DataBind();
    }
    #endregion
}
