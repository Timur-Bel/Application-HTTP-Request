using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;

namespace ConsoleAppTestTask
{
    class Program
    {
        public static string GetUrl(string address)

        {

            WebClient client = new WebClient();

            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            return client.DownloadString(address);

            

        }

        static List<string> GetHref(string html)
        {
            string[] d = html.Split('\n');

            List<string> HRefs = new List<string>();
            string Buf = "";

            foreach (string str in d)
            {
                for (int i = 0; i < str.Length - 4; i++)
                {
                    if (str.Substring(i, 4) == "href")
                    {
                        Buf = "";
                        i += 6;
                        while (str.Substring(i, 1) != @"""")
                        {
                            Buf += str.Substring(i, 1);
                            i++;
                        }
                    }
                    if (Add(HRefs, Buf)) HRefs.Add(Buf);


                }

            }
            return HRefs;

        }
        static bool Add(List<string> lis, string str)
        {
            bool a = true;

            if (str.Length > 1)
            {
                string k = str.Substring(0, 1);

                if (k == "#") return false;
            }
            if (str != "")
            {
                for (int j = 0; j < lis.Count; j++)
                    if (lis[j] == str) { a = false; break; }
            }
            else
                a = false;
            return a;
        }

        static string getText(string html)
        {
            string s = "";
            bool b = true;
            foreach (char c in html)
                switch (c)
                {
                    case '<': b = false; break;
                    case '>': b = true; break;

                    default:

                        if (b) s += c;
                        break;
                }
            return s;

        }

        static void Main(string[] args)
        {



            string url;
            String xmlurl;
            Console.WriteLine("Please enter the url: ");
            url = Convert.ToString(Console.ReadLine());
            xmlurl = url;
            XmlTextReader reader = new XmlTextReader(xmlurl);

            var a = GetUrl(url);

            List<string> R = GetHref(a);

            DateTime one = DateTime.Now;
            
            Thread.Sleep(3000);
            DateTime two = DateTime.Now;
            TimeSpan result = two - one;


            foreach (string s in R)
                Console.WriteLine("Html Documents URLS: " + getText(s) + " - sec "+ result.Seconds.ToString());


            Console.ReadKey();

            while(reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: 
                        Console.Write("Xmls Url -  "+"<" + reader.BaseURI);

                        while (reader.MoveToNextAttribute()) 
                            Console.Write("    " + getText(reader.BaseURI) + reader.Value + " - sec " + result.Seconds.ToString());
                        Console.WriteLine("  ");
                        Console.Write("  ");

                        break;

                       


                }
            }

            Console.ReadKey();

        }
      }
   }




        
    










        
    


