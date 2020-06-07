using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Parser
{
    class Program
    {
        //static ITelegramBotClient botClient;
        static string recentMessage = "";

        static void Main(string[] args)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            //System.Timers.Timer t = new System.Timers.Timer(1);
            //t.AutoReset = true;
            //t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //t.Start();

            //async void OnTimedEvent(Object source, ElapsedEventArgs e)
            //{
            //    await GetHtmlAsync();
            //    await SendToBotAsync();
            //}


            await GetHtmlAsync();
            SendToBotAsync();


            //#region TELEGRAMBOT

            //botClient = new TelegramBotClient("1139402041:AAHbmIdDK1XaIXVhilFOgNF9uiAkHmj667c");

            //var me = botClient.GetMeAsync().Result;
            //Console.WriteLine(
            //  $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            //);

            //botClient.OnMessage += Bot_OnMessage;
            //botClient.StartReceiving();
            //Thread.Sleep(int.MaxValue);

        }

        private static async Task SendToBotAsync()
        {

            //string recentMessage = "";


            var bot = new TelegramBotClient("1139402041:AAHbmIdDK1XaIXVhilFOgNF9uiAkHmj667c");
            var t = await bot.SendTextMessageAsync("@neravnodushny_bot", recentMessage);
        }

        //#endregion

        //static async void Bot_OnMessage(object sender, MessageEventArgs e)
        //{
        //    if (e.Message.Text != null)
        //    {
        //        Console.WriteLine($"Received a text message in chat {e.Message.Chat.Id}.");

        //        await botClient.SendTextMessageAsync(
        //          chatId: e.Message.Chat,
        //          text: "You said:\n" + e.Message.Text
        //        );
        //    }
        //}

        //        Message message = await botClient.SendTextMessageAsync(
        //  chatId: e.Message.Chat, // or a chat id: 123456789
        //  text: "Trying *all the parameters* of `sendMessage` method",
        //  parseMode: ParseMode.Markdown,
        //  disableNotification: true,
        //  replyToMessageId: e.Message.MessageId,
        //  replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
        //    "Check sendMessage method",
        //    "https://core.telegram.org/bots/api#sendmessage"
        //  ))
        //);

        public static async Task GetHtmlAsync()
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

            //var rawHlinkList = messagesListHtml[0].Descendants("href").ToList();

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
                    string x = HttpUtility.HtmlDecode(i.InnerText.TrimStart(':', ' ').Replace(" &nbsp;", " ").Replace("&#133","").Replace(">", "\"").Replace("<", "\""));

                    recentMessage = x;
                    Console.WriteLine(x);
                    Console.WriteLine();
                }
            }



            

                //Console.WriteLine(m);
                //Console.WriteLine();
            
            
            //Console.WriteLine(m);


        }


        #region TIMER


        #endregion


    }
}