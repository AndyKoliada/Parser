using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Parser
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main(string[] args)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");
            GetHtmlAsync();

            //static ITelegramBotClient botClient;


            #region TELEGRAMBOT

            botClient = new TelegramBotClient("1139402041:AAHbmIdDK1XaIXVhilFOgNF9uiAkHmj667c");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            //botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);


            Console.ReadLine();
        }

                #endregion

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "You said:\n" + e.Message.Text
                );
            }
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

            var rawHlinkList = messagesListHtml[0].Descendants("href").ToList();

            //var rawTimecodeList = messagesListHtml[0].Descendants("#text").ToList();

            //var messageList = ;

            //int messageIndex = 0;

            //var recentMessage = rawMessagesList[messageIndex];

            //string m = HttpUtility.HtmlDecode(recentMessage.InnerText.TrimStart(':',' ').Replace(" &nbsp;", " "));


           foreach(var i in rawMessagesList)
            {
                //string x = HttpUtility.HtmlDecode(i.InnerText.TrimStart(':', ' ').Replace(" &nbsp;", " "));
                if(i.InnerText.StartsWith(':'))
                {
                    string x = HttpUtility.HtmlDecode(i.InnerText.TrimStart(':', ' ').Replace(" &nbsp;", " ").Replace("&#133",""));
                    Console.WriteLine(x);
                    Console.WriteLine();
                }
            }

            

                //Console.WriteLine(m);
                //Console.WriteLine();
            
            
            //Console.WriteLine(m);


        }


        #region TIMER
        //public void Test()
        //{
        //    Timer t = new Timer(5000);
        //    t.AutoReset = true;
        //    t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        //    t.Start();
        //}

        //private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{

        //}
        #endregion


    }
}