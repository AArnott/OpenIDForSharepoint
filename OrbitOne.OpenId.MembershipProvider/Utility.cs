using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OrbitOne.OpenId.MembershipProvider
{
   internal class Utility
    {

        private static readonly string EVENT_SOURCE = "OpenIDMembershipProvider";
        private static readonly string EVENT_LOG = "Application";



       public static string GetConfigValue(string configValue, string defaultValue)
       {
           if (String.IsNullOrEmpty(configValue))
               return defaultValue;

           return configValue;
       }

       public static string GenerateNewSalt()
       {
           byte[] saltInBytes = new byte[16];
           System.Security.Cryptography.RNGCryptoServiceProvider saltGenerator = new System.Security.Cryptography.RNGCryptoServiceProvider();
           saltGenerator.GetBytes(saltInBytes);
           return Convert.ToBase64String(saltInBytes);
       }

       public static string GenerateNewGUID()
       {
           return Guid.NewGuid().ToString();
       }

       public static byte[] HexToByte(string hexString)
       {
           byte[] returnBytes = new byte[hexString.Length / 2];
           for (int i = 0; i < returnBytes.Length; i++)
               returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
           return returnBytes;
       }


       public static void WriteToEventLog(Exception e, string action)
       {
           EventLog log = new EventLog();
           log.Source = EVENT_SOURCE;
           log.Log = EVENT_LOG;

           string message = "An exception occurred communicating with the data source.\n\n";
           message += "Action: " + action + "\n\n";
           message += "Exception: " + e.ToString();

           log.WriteEntry(message);
       }


    }
}
