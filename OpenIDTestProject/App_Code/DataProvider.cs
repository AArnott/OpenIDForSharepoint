using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Security;
using OrbitOne.OpenId.MembershipProvider;

/// <summary>
/// Summary description for DataProvider
/// </summary>
namespace OrbitOne.OpenId.TestProject{
    public class DataProvider
    {
        protected OpenIdMembershipProvider OpenIdMembershipProvider
        {
            get
            {
                OpenIdMembershipProvider opmp = (OpenIdMembershipProvider)Membership.Providers["OpenIDMembershipProvider"];
                if(opmp!=null)
                    return opmp;
                else 
                    throw new ApplicationException("OpenIdMembershipProvider not found!");
            }
        }

        
        #region Users

        public void DeleteUser(String userName)
        {
            RemoveUserFromAllRoles(userName);
            OpenIdMembershipProvider.DeleteUser(userName, true);
        }

        public MembershipUser GetUser(string userName)
        {
           return OpenIdMembershipProvider.GetUser(userName, true);    
        }

        public MembershipUserCollection GetsAllUsers(int totalRecords)
        {
           return OpenIdMembershipProvider.GetAllUsers(0, int.MaxValue, out totalRecords);
        }

        public IList<OpenIdMembershipUser> GetsAllOpenIdUsers(int totalRecords)
        {
            return OpenIdMembershipProvider.GetAllOpenIdUsers(out totalRecords);
        }

        #endregion
      
        #region Roles

        public DataSet GetsAllRoles()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add();
            ds.Tables[0].Columns.Add("RoleName");
            ArrayList al = new ArrayList(Roles.GetAllRoles());

            for (int i = 0; i < al.Count; i++)
            {
                ds.Tables[0].Rows.Add(al[i]);
            }
            return ds;
        }


        public void AddRoles(string roleName)
        {
            if (roleName != null && !Roles.RoleExists(roleName))
            {
                Roles.CreateRole(roleName);
            }
        }


        public void AddUsersToRoles(string userName, string roleName)
        {
            if (userName != null && roleName != null)
            {
                String[] userNames = {userName};
                Roles.AddUsersToRole(userNames, roleName);
            }
        }


        public void DeleteUsersFromRoles(string userName, string roleName)
        {
            if (userName != null && roleName != null)
            {
                Roles.RemoveUserFromRole(userName, roleName);
            }
        }


        public DataSet GetRolesForUser(string userName)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add();


            if (userName != null)
            {
                ds.Tables[0].Columns.Add("RoleName");

                ArrayList al = new ArrayList(Roles.GetRolesForUser(userName));

                for (int i = 0; i < al.Count; i++)
                {
                    ds.Tables[0].Rows.Add(al[i]);
                }
            }


            return ds;
        }

        private void RemoveUserFromAllRoles(String userName)
        {
            string[] roles = Roles.GetRolesForUser(userName);
            if (roles.Length > 0)
            {
                Roles.RemoveUserFromRoles(userName, roles);
            }
        }

        #endregion

    }
}