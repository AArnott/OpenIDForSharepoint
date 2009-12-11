using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace OrbitOne.OpenId.MembershipProvider
{
    public class OpenIdMembershipProvider : System.Web.Security.MembershipProvider
    {
        #region Members

        private static readonly OpenIdRelyingParty consumer = new OpenIdRelyingParty();
        
        private static readonly string EXCEPTION_MESSAGE = "An exception occurred. Please check the Event Log.";
        private string _connectionString;
        private bool _WriteExceptionsToEventLog;
        private string _applicationName;
        private ClaimsResponse _sreg;
        private string _optionalInformation;
        private string _loginURL;
        private int _minutesSinceLastActivity;
        private string _nonOpenIdMembershipProviderName;
        #endregion

        #region Properties

      

        public int MinutesSinceLastActivity
        {
            get { return _minutesSinceLastActivity; }
        }

        
        public string LoginURL
        {
            get { return _loginURL; }
        }

       
        public string Nickname
        {
            get { return _sreg != null ? _sreg.Nickname : string.Empty; }
        }

        
        public string Email
        {
            get { return _sreg != null ? _sreg.Email : string.Empty; }
        }

      
        public string Fullname
        {
            get { return _sreg != null ? _sreg.FullName : string.Empty; }
        }

       
        public string Dob
        {
            get { return _sreg != null ? _sreg.BirthDateRaw : string.Empty; }
        }


        public string Gender
        {
            get
            {
                if (_sreg != null && _sreg.Gender.HasValue)
                {
                    return _sreg.Gender.Value == DotNetOpenAuth.OpenId.Extensions.SimpleRegistration.Gender.Male ? "M" : "F";
                }
                else
                {
                    return string.Empty;
                }
            }
        }

       
        public string Postcode
        {
            get { return _sreg != null ? _sreg.PostalCode : string.Empty; }
        }

       
        public string Country
        {
            get { return _sreg != null ? _sreg.Country : string.Empty; }
        }

        
        public string Language
        {
            get { return _sreg != null ? _sreg.Language : string.Empty; }
        }

        
        public string Timezone
        {
            get { return _sreg != null ? _sreg.TimeZone : string.Empty; }
        }

       
        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

       
        public bool WriteExceptionsToEventLog
        {
            get { return _WriteExceptionsToEventLog; }
            set { _WriteExceptionsToEventLog = value; }
        }



        public string NonOpenIdMemebershipProviderName
        {
            get { return _nonOpenIdMembershipProviderName; }
            set { _nonOpenIdMembershipProviderName = value; }
        }


        public System.Web.Security.MembershipProvider NonOpenIdMembershiProvider
        {
            get
            {

                System.Web.Security.MembershipProvider provider = Membership.Providers[NonOpenIdMemebershipProviderName];
                if (provider != null)
                { return provider; }
                else
                { throw new ApplicationException("NonOpenIdMembershiProvider does not exist!"); }
            }
        }




        #endregion

        #region Initialize
        
        public override void Initialize(string name, NameValueCollection config)
        {

            try
            {
                // Initialize values from web.config.
                if (config == null)
                    throw new ArgumentNullException("config");

                if (name == null || name.Length == 0)
                    name = "OpenIDMembershipProvider";

                if (String.IsNullOrEmpty(config["description"]))
                {
                    config.Remove("description");
                    config.Add("description", "OpenID Membership Provider");
                }

                // Initialize the abstract base class.
                base.Initialize(name, config);

                _applicationName = Utility.GetConfigValue(config["applicationName"], System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
                _WriteExceptionsToEventLog = Convert.ToBoolean(Utility.GetConfigValue(config["writeExceptionsToEventLog"], "false"));

                // Initialize the OpenID Attributes
                _optionalInformation = Utility.GetConfigValue(config["OptionalInformation"], "");
                NonOpenIdMemebershipProviderName = Utility.GetConfigValue(config["NonOpenIdMembershipProviderName"], "");
                _minutesSinceLastActivity = int.Parse(Utility.GetConfigValue(config["MinutesSinceLastActivity"], "20"));
               

                // Initialize the SqlConnection.
                ConnectionStringSettings ConnectionStringSettings =
                  ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

                if (ConnectionStringSettings == null || ConnectionStringSettings.ConnectionString.Trim() == "")
                {
                    throw new ProviderException("Connection string cannot be blank.");
                }

                _connectionString = ConnectionStringSettings.ConnectionString;


             
            }
            catch (Exception e)
            {
                if (this.WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(e, "Initialize");
                }

                throw new OpenIdMembershipProviderException("Initialize failed.", e);
            }
        }
               
        #endregion

        #region MembershipProvider Methods

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
                                                    bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {

          
            MembershipUser u = GetUserByOpenId(username, false);
            if (u == null)
            {
                u = NonOpenIdMembershiProvider.CreateUser(username, password, email, passwordQuestion, passwordAnswer,
                                                isApproved, providerUserKey, out status);
                LinkUserWithOpenId(username, u.ProviderUserKey);
                return u;
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }
        
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("OpenId_DeleteUserOpenIdLink", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@userId", SqlDbType.VarChar, 255).Value = NonOpenIdMembershiProvider.GetUser(username, false).ProviderUserKey.ToString();

            try
            {

                conn.Open();
                cmd.ExecuteNonQuery();
                return NonOpenIdMembershiProvider.DeleteUser(username, deleteAllRelatedData);


            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(e, "DeleteUser");
                }

                throw new OpenIdMembershipProviderException("DeleteUser failed.", e);
            }
            finally
            {
                conn.Close();
            }


        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            return NonOpenIdMembershiProvider.GetAllUsers(pageIndex, pageSize, out  totalRecords);
        }
        
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            return NonOpenIdMembershiProvider.GetUser(username, userIsOnline);
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            return NonOpenIdMembershiProvider.GetUser(providerUserKey, userIsOnline);
        }
            
         
        public override int GetNumberOfUsersOnline()
        {
            return NonOpenIdMembershiProvider.GetNumberOfUsersOnline();
        }
    

        public override void UpdateUser(MembershipUser user)
        {

            NonOpenIdMembershiProvider.UpdateUser(user);
        }

        public override bool ValidateUser(string username, string password)
        {
            _loginURL = HttpContext.Current.Request.Url.AbsoluteUri;

            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            try
            {
                var request = consumer.CreateRequest(username);
                if (request == null)
                {
                    // Not a valid OpenID.
                    return false;
                }

                // The following illustrates how to use SREG.
                // TODO: use _optionalInformation contents instead of hard-coding it.
                request.AddExtension(new ClaimsRequest
                {
                    Email = DemandLevel.Request,
                });

                // Redirect the user to the OpenID provider Page
                request.RedirectToProvider();
                return true; // never executed because Redirect throws an exception, but C# requires it.
            }
            catch (ProtocolException fexc)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(fexc, "ValidateOpenIDUser");
                }

                throw new OpenIdMembershipProviderException("ValidateUser failed.", fexc);
            }
        }
      
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            return NonOpenIdMembershiProvider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
        }
        #endregion MembershipProvider Methods

        #region OpenId Related

        public bool ValidateOpenIDUser()
        {
            try
            {
                var response = consumer.GetResponse();
                if (response.Status != AuthenticationStatus.Authenticated)
                {
                    return false;
                }

                _sreg = response.GetExtension<ClaimsResponse>();

                MembershipUser user = GetUserByOpenId(response.ClaimedIdentifier, true);
                if (user != null)
                {
                    FormsAuthentication.RedirectFromLoginPage(user.UserName, false);
                    return true; // never reached, due to redirect
                }
                else
                {
                    throw new OpenIdNotLinkedException(response.ClaimedIdentifier);
                }
            }
            catch (ProtocolException ex)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(ex, "ValidateOpenIDUser");
                }

                return false;
            }
            catch (OpenIdNotLinkedException nlEx)
            {
                throw nlEx;
            }
        }

        public MembershipUser GetUserByOpenId(string openId, bool userIsOnline)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("OpenId_GetUserIdByOpenId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClaimedIdentifier", SqlDbType.NVarChar).Value = openId;
            SqlDataReader reader = null;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    Guid userId = reader.GetGuid(0);
                    return NonOpenIdMembershiProvider.GetUser(userId, userIsOnline);
                }

            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(e, "GetUserByOpenId ");
                }
                throw new OpenIdMembershipProviderException("GetUserByOpenId failed.", e);
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return null;
        }
        public IList<OpenIdMembershipUser> GetAllOpenIdUsers(out int totalRecords)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("OpenId_Membership_GetAllUsers", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ApplicationName", SqlDbType.VarChar, 255).Value = ApplicationName;
            IList<OpenIdMembershipUser> users = new List<OpenIdMembershipUser>();
            SqlDataReader reader = null;
            totalRecords = 0;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    OpenIdMembershipUser u = GetUserFromReader(reader);
                    users.Add(u);
                    totalRecords++;
                }
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(e, "GetAllUsers ");

                    throw new ProviderException(EXCEPTION_MESSAGE);
                }
                throw new OpenIdMembershipProviderException("GetAllOpenIdUsers failed.", e);
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }


            return users;
        }
        public void LinkUserWithOpenId(Identifier claimedIdentifier, object userId)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("OpenId_LinkUserWithOpenId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClaimedIdentifier", SqlDbType.NVarChar).Value = claimedIdentifier;
            cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = userId.ToString();
            try
            {

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(e, "LinkUserWithOpenId ");
                }
                throw new OpenIdMembershipProviderException("LinkUserWithOpenId failed.", e);
            }
            finally
            {

                conn.Close();
            }
        }
        public void RemoveUserOpenIdLinkByOpenId(Identifier claimedIdentifier)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("OpenId_DeleteUserOpenIdLink", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClaimedIdentifier", SqlDbType.NVarChar).Value = claimedIdentifier;
            cmd.Parameters.Add("@userId", SqlDbType.UniqueIdentifier).Value = DBNull.Value;
            try
            {

                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(e, "LinkUserWithOpenId ");
                }
                throw new OpenIdMembershipProviderException("RemoveUserOpenIdLinkByOpenId failed.", e);
            }
            finally
            {

                conn.Close();
            }
        }
        public void RemoveUserOpenIdLinkByUserId(object userId)
        {

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("OpenId_DeleteUserOpenIdLink", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ClaimedIdentifier", SqlDbType.NVarChar).Value = DBNull.Value;
            cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = userId.ToString();
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(e, "LinkUserWithOpenId ");
                }
                throw new OpenIdMembershipProviderException("RemoveUserOpenIdLinkByUserId failed.", e);
            }
            finally
            {

                conn.Close();
            }
        }
        public IList<string> GetOpenIdsByUserName(string userName)
        {
            object userId = null;

            userId = NonOpenIdMembershiProvider.GetUser(userName, false).ProviderUserKey;

            if (userId == null)
                return null;

            IList<string> openIds = new List<string>();

            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand("OpenId_GetOpenIdsByUserId", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@userId", SqlDbType.NVarChar).Value = userId.ToString();
            SqlDataReader reader;
            try
            {

                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    openIds.Add(reader.GetString(0));
                }

            }
            catch (SqlException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    Utility.WriteToEventLog(e, "GetOpenIdsByUserName ");
                }
                throw new OpenIdMembershipProviderException("GetOpenIdsByUserName failed.", e);
            }
            finally
            {

                conn.Close();
            }
            return openIds;
        }
        private OpenIdMembershipUser GetUserFromReader(SqlDataReader reader)
        {

            string username = reader.GetString(0);

            string openId = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
            string email = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
            string passwordQuestion = "";
            if (reader.GetValue(3) != DBNull.Value)
                passwordQuestion = reader.GetString(3);

            string comment = "";
            if (reader.GetValue(4) != DBNull.Value)
                comment = reader.GetString(4);

            bool isApproved = reader.GetBoolean(5);

            DateTime creationDate = reader.GetDateTime(6);

            DateTime lastLoginDate = new DateTime();
            if (reader.GetValue(7) != DBNull.Value)
                lastLoginDate = reader.GetDateTime(7);

            DateTime lastActivityDate = reader.GetDateTime(8);

            DateTime lastPasswordChangedDate = reader.GetDateTime(9);

            object providerUserKey = reader.GetValue(10);

            bool isLockedOut = reader.GetBoolean(11);

            DateTime lastLockedOutDate = new DateTime();
            if (reader.GetValue(12) != DBNull.Value)
                lastLockedOutDate = reader.GetDateTime(12);

            OpenIdMembershipUser u = new OpenIdMembershipUser(
                                                  this.Name,
                                                  openId,
                                                  username,
                                                  providerUserKey,
                                                  email,
                                                  passwordQuestion,
                                                  comment,
                                                  isApproved,
                                                  isLockedOut,
                                                  creationDate,
                                                  lastLoginDate,
                                                  lastActivityDate,
                                                  lastPasswordChangedDate,
                                                  lastLockedOutDate);


            return u;
        }
        
        #endregion OpenId Related

        #region Not Suported Methods
        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 0; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 0; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotSupportedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 0; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 0; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return ""; }
        }


        public override bool ChangePassword(string username, string oldPwd, string newPwd)
        {
            throw new NotSupportedException();
        }


        public override bool ChangePasswordQuestionAndAnswer(string username,
                      string password,
                      string newPwdQuestion,
                      string newPwdAnswer)
        {
            throw new NotSupportedException();
        }


        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }


        public override bool UnlockUser(string username)
        {
            throw new NotSupportedException();
        }


        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }


        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }


        private void UpdateFailureCount(string username, string failureType)
        {
            throw new NotSupportedException();
        }


        private bool CheckPassword(string password, string dbpassword)
        {
            throw new NotSupportedException();
        }


        private string EncodePassword(string password)
        {
            throw new NotSupportedException();
        }


        private string UnEncodePassword(string encodedPassword)
        {
            throw new NotSupportedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
