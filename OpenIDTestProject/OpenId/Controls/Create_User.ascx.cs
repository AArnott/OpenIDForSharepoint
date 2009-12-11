using System;
using System.Configuration;
using System.Web.Security;

public partial class Open_Id_Controls_Create_User : System.Web.UI.UserControl
{
    private string GroupName;   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Page.User.IsInRole("Administrator"))
            {
                CreateUserWizard.LoginCreatedUser = false;
                GroupName = GetDefaultRoleForNewUser();
            }
        }
    }

  
    #region

    protected void ContinueButton_Click(object sender, EventArgs e)
    {
        Response.Redirect(".\\About.aspx");
    }

    
    private string GetDefaultRoleForNewUser()
    {
        if (ConfigurationManager.AppSettings["DefaultRoleForNewUser"] == null)
        {
            throw (new Exception("DefaultRoleForNewUser was not been defined in the appsettings section of config"));
        }
        else
        {
            string defaultRole = ConfigurationManager.AppSettings["DefaultRoleForNewUser"];
            if (string.IsNullOrEmpty(defaultRole))
            {
                throw (new Exception("DefaultRoleForNewUser does not contain a default value"));
            }
            else
            {
                if (string.Compare(defaultRole, "2") < 0 && string.Compare(defaultRole, "0") >= 0)
                {
                    return (ConfigurationManager.AppSettings["DefaultRoleForNewUser"]);
                }
                else
                {
                    throw (new ArgumentException("DefaultRoleForNewUser defined in the appsettings has to be between 0 and 1"));
                }
            }
        }
    }

  
    protected void AddUserToRole(string newUserName, string roleInformation)
    {
        switch (roleInformation)
        {
            case ("0"):
                Roles.AddUserToRole(newUserName, "Administrator");
                Roles.AddUserToRole(newUserName, "SiteRole1");
                break;

            case ("1"):
                Roles.AddUserToRole(newUserName, "SiteRole1");
                break;

            default:
                Roles.AddUserToRole(newUserName, "SiteRole1");
                break;
        }
    }

  
    protected void CreateUserWizard_CreatedUser(object sender, EventArgs e)
    {
        AssignUserToRole(CreateUserWizard.UserName);
    }

    private void AssignUserToRole(String userName)
    {
        GroupName = GetDefaultRoleForNewUser();

        if (GroupName.CompareTo("Administrator") == 0)
        {
            AddUserToRole(userName, "0");
        }
        else
        {
            if (GroupName.CompareTo("SiteRole1") == 1)
            {
                AddUserToRole(userName, "1");
            }
            else
            {
                AddUserToRole(userName, GetDefaultRoleForNewUser());
            }
        }
    }  
    
    protected void CreateUserWizard1_CreatedUser(object sender, EventArgs e)
    {
        AssignUserToRole(CreateUserWizard1.UserName);
    }
   
    #endregion

  
}
