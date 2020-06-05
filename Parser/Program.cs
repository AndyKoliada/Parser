using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            GetHtmlAsync();

            Console.ReadLine();
        }

        private static async void GetHtmlAsync()
        {
            string url = "https://dumskaya.net/user/neravnoduschnyj/";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);



            var messagesListHtml = htmlDocument.DocumentNode.Descendants("td")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("newscol hideprint")).ToList();



            var rawMessagesList = messagesListHtml[0].Descendants("#text").ToList();

            int messageIndex = 4;

            var recentMessage = rawMessagesList[messageIndex];

            string m = HttpUtility.HtmlDecode(recentMessage.InnerText.TrimStart(':',' ').Replace(" &nbsp;", " "));


           
            

                Console.WriteLine(m);
                Console.WriteLine();
            
            
            //Console.WriteLine(m);


        }

        //private static messageFormat()
    }
}