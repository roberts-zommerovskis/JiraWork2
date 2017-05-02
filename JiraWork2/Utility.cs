using System;
using System.Text;

namespace JiraWork2
{
    class Utility
    {
        public static string GetEncodedCredentials(string UserName, string Password)
        {
            string mergedCredentials = String.Format("{0}:{1}", UserName, Password);
            byte[] byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
    }
}