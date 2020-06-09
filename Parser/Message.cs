using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
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

            for (int i = 13; i < rawMessagesList.Count-1; i++)
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



            //for(int m = MessagesList.Count - 1; m > MessagesList.Count-1;)
            //{
            //    if (MessagesList[m] != Telegram.RecentMessage)
            //    {
            //        Telegram.RecentMessage = MessagesList[m];
            //        Console.WriteLine(m);
            //        //await Telegram.SendToBotAsync();
            //        m--;
            //    }

            //}

            // Console.WriteLine(rawMessagesList[i].OuterHtml);


            //var rawMessagesList = messagesListHtml[0].Descendants("#text").ToList();

            //var rawNewsList = messagesListHtml[0].Descendants("#text").ToList();

            //var rawNewsHlinkList = messagesListHtml[0].Descendants("#text").ToList();

            //var rawTimeCodeList = messagesListHtml[0].Descendants("#text").ToList();


            //for (int i = 0; i < rawMessagesList.Count; i++)
            //{
            //    if (!(rawMessagesList[i].InnerHtml == "") || (rawMessagesList[i].InnerHtml == "Комментарии которые оставил Неравнодушный:"))
            //    {
            //        if (rawMessagesList[i].FirstChild == "")
            //    }

            //    else if ()
            //    {

            //    }


            //    else if ()
            //    {

            //    }

            //    RecentMessage = x;
            //    Console.WriteLine(x);
            //    await SendToBotAsync();
            //}

            //Console.WriteLine(HttpUtility.HtmlDecode(rawMessagesList[10].InnerHtml));

        }

        

    }
}
