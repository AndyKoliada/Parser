using System;
using System.Threading.Tasks;
using System.Timers;

namespace Parser
{
    class Program
    {
        //https://api.telegram.org/bot1139402041:AAHbmIdDK1XaIXVhilFOgNF9uiAkHmj667c/getUpdates
        // GET chat ID

        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
        }

        private static async Task MainAsync()
        {

            await Message.GetHtmlAsync();

            System.Timers.Timer t = new System.Timers.Timer(60000);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            t.Start();

            async void OnTimedEvent(Object source, ElapsedEventArgs e)
            {
                await Message.GetHtmlAsync();
            }
        }

    }
}