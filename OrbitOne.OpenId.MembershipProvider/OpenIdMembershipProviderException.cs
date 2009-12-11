using System;
using System.Collections.Generic;
using System.Text;

namespace OrbitOne.OpenId.MembershipProvider
{
    class OpenIdMembershipProviderException : Exception
    {
        public OpenIdMembershipProviderException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

