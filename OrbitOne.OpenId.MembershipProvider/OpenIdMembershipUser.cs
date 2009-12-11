using System;
using System.Web.Security;
using DotNetOpenAuth.OpenId;

namespace OrbitOne.OpenId.MembershipProvider
{
    public class OpenIdMembershipUser : MembershipUser
    {
        public Identifier OpenId { get; set; }

        public OpenIdMembershipUser(
          string providerName, 
          Identifier openId,
          string name,
          object providerUserKey,
          string email,
          string passwordQuestion,
          string comment,
          bool isApproved,
          bool isLockedOut,
          DateTime creationDate,
          DateTime lastLoginDate,
          DateTime lastActivityDate,
          DateTime lastPasswordChangedDate,
          DateTime lastLockoutDate)
            : base(
          providerName,
          name,
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
          lastLockoutDate)
        {
            OpenId = openId;
        }
    }
}
