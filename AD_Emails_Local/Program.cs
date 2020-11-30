using System;
using System.Collections;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace AD_Emails_Local
{
    class Program
    {
        static void Main(string[] args)
        {
            const string DOMAIN = "YOUR_DOMAIN.local";
            ArrayList usersWithOutEmails = new ArrayList();
            ArrayList usersWithEmails = new ArrayList();
            using (var context = new PrincipalContext(ContextType.Domain, DOMAIN))
            {
                using (var searcher = new PrincipalSearcher(new UserPrincipal(context)))
                {
                    foreach (var result in searcher.FindAll())
                    {
                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;

                        string email = (string)de.Properties["mail"].Value;
                        if(email != null)
                        {
                            string first = (string)de.Properties["givenName"].Value;
                            string last = (string)de.Properties["sn"].Value;
                            if(first != null && last != null)
                            {
                                string name = first + " " + last + " - " + email;
                                usersWithEmails.Add(name);
                            }
                        } else
                        {
                            string first = (string)de.Properties["givenName"].Value;
                            string last = (string)de.Properties["sn"].Value;
                            if (first != null && last != null)
                            {
                                string name = first + " " + last + " - NO EMAIL";
                                usersWithOutEmails.Add(name);
                            }
                        }
                    }
                }
            }
            Console.WriteLine("===============================================");
            Console.WriteLine("Users With Emails");
            Console.WriteLine("===============================================");
            foreach(string name in usersWithEmails)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("===============================================");
            Console.WriteLine();
            Console.WriteLine("===============================================");
            Console.WriteLine("Users Without Emails");
            Console.WriteLine("===============================================");
            foreach (string name in usersWithOutEmails)
            {
                Console.WriteLine(name);
            }

            Console.ReadLine();
        }
    }
}
