using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraWork2
{
    class Program
    {
        static void Main(string[] args)
        {
            Jira objJira = new Jira();
            objJira.Url = "https://jiratest1.atlassian.net/";
            objJira.JsonString = @"{""fields""      :     {
                                    ""project""     :     {
                                    ""key""         :       ""JT""                                  },
                                    ""summary""     :       ""TEST ISSUE NAME""                                           ,
                                    ""description"" :       ""Another Test Case""                                       ,
                                    ""issuetype""   :     { 
                                    ""name""        :       ""Story""                                          }}}";
            objJira.UserName = "roberts.zommerovskis@gmail.com";
            objJira.Password = "Mangali$93";
            objJira.AddJiraIssue();
            objJira.ListProjectIssues();
        }
    }
}
