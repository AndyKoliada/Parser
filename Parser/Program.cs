using HtmlAgilityPack;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using Telegram.Bot;

namespace Parser
{
    class Program
    {

        //https://api.telegram.org/bot1139402041:AAHbmIdDK1XaIXVhilFOgNF9uiAkHmj667c/getUpdates
        // GET chat ID

        private static string recentMessage = "Some error occured, sorry";

        public static string RecentMessage { get => recentMessage; set => recentMessage = value; }

        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");

            System.Timers.Timer t = new System.Timers.Timer(60 * 1000);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            t.Start();

            async void OnTimedEvent(Object source, ElapsedEventArgs e)
            {
                await GetHtmlAsync();
                await SendToBotAsync();
            }
            await SendToBotAsync();
        }

        private static async Task SendToBotAsync()
        {


            var bot = new TelegramBotClient("1139402041:AAHbmIdDK1XaIXVhilFOgNF9uiAkHmj667c");
            var t = await bot.SendTextMessageAsync("-455430398", RecentMessage);
        }

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



            foreach (var i in rawMessagesList)
            {
                
                if (i.InnerText.StartsWith(':'))
                {
                    string x = HttpUtility
                    .HtmlDecode(i.InnerText.TrimStart(':', ' ')
                    .Replace(" &nbsp;", " ")
                    .Replace("&#133", "")
                    .Replace(">", "\"")
                    .Replace("<", "\""));

                    RecentMessage = x;
                    Console.WriteLine(x);

                }
            }

        }

    }
}