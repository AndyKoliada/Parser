using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Parser
{
    class Message
    {
        public static async Task GetHtmlAsync()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            string url = "https://dumskaya.net/user/neravnoduschnyj/";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            var messagesListHtml = htmlDocument.DocumentNode.Descendants("td")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("newscol hideprint")).ToList();

            var rawMessagesList = messagesListHtml[0].Descendants().ToList();

            Queue<string> MessagesList = new Queue<string>();

            for (int i = 0; i < rawMessagesList.Count-1; i++)
            {
                if (rawMessagesList[i].InnerLength != 0 && rawMessagesList[i].InnerText.StartsWith(':'))
                {
                    string x = HttpUtility
                    .HtmlDecode(rawMessagesList[i].InnerText.TrimStart(':', ' ')
                    .Replace(" &nbsp;", " ")
                    .Replace("&#133", "")
                    .Replace(">", "\"")
                    .Replace("<", "\""));

                    MessagesList.Enqueue(x);
                }
            }

            Telegram.RecentMessage = MessagesList.Dequeue();
            if(Telegram.RecentMessage != Telegram.PreviousMessage)
            {
                await Telegram.SendToBotAsync();
                Console.WriteLine(Telegram.RecentMessage);
                Telegram.PreviousMessage = Telegram.RecentMessage;
            }


        }

        

    }
}
