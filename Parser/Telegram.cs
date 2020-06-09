using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Parser
{
    class Telegram
    {
        private static string recentMessage = "Some error occured, sorry";

        public static string RecentMessage { get => recentMessage; set => recentMessage = value; }

        private static async Task SendToBotAsync()
        {
            var bot = new TelegramBotClient("1139402041:AAHbmIdDK1XaIXVhilFOgNF9uiAkHmj667c");
            var t = await bot.SendTextMessageAsync("-455430398", RecentMessage);
        }

    }
}
