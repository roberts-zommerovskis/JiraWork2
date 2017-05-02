using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace JiraWork2
{
    class Jira
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string JsonString { get; set; }
        public IEnumerable<string> filePaths { get; set; }
        

        public void AddJiraIssue()
        {
            string restUrl = String.Format("{0}rest/api/2/issue/", Url);
            HttpWebResponse response = null;
            HttpWebRequest request = WebRequest.Create(restUrl) as HttpWebRequest;
            request.Method = "POST";
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Basic " + Utility.GetEncodedCredentials(UserName, Password));
            byte[] data = Encoding.UTF8.GetBytes(JsonString);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();
            }
            using (response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    string str = reader.ReadToEnd();
                    Console.WriteLine("The server returned '{0}'\n{1}", response.StatusCode, str);
                    Console.WriteLine("\n\nPress ENTER for Issue list!");
                    Console.ReadLine();            
                }
            }
            request.Abort();
        }

        public void ListProjectIssues()
        {
            string restUrl = String.Format("{0}rest/api/2/search?jql=project=JT", Url);
            HttpWebRequest request = WebRequest.Create(restUrl) as HttpWebRequest;
            request.Method = "GET";
            request.Accept = "application/json";
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "Basic " + Utility.GetEncodedCredentials(UserName, Password));
            request.PreAuthenticate = true;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                Console.WriteLine(GetIssueSummaries(reader.ReadToEnd(), "\"summary\":", ",\"creator\""));
                Console.ReadLine();
            }
        }
        public static string GetIssueSummaries(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
    }
}