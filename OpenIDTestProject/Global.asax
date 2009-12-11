<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {   
        if (Roles.Enabled)
        {
            String administrator = "Administrator";
            String siteRole1 = "SiteRole1";
                      
            if (!Roles.RoleExists(administrator))
            {
                Roles.CreateRole(administrator);
            }
            if (!Roles.RoleExists(siteRole1))
            {
                Roles.CreateRole(siteRole1);
            } 
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
       
    }
        
    void Application_Error(object sender, EventArgs e) 
    {        

    }

    void Session_Start(object sender, EventArgs e) 
    {      

    }

    void Session_End(object sender, EventArgs e) 
    {       

    }
       
</script>
