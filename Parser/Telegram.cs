using System.Threading.Tasks;
using Telegram.Bot;

namespace Parser
{
    class Telegram
    {
        private static string recentMessage = "recentMessage";
        private static string previousMessage = "previousMessage";

        public static string RecentMessage { get => recentMessage; set => recentMessage = value; }
        public static string PreviousMessage { get => previousMessage; set => previousMessage = value; }

        public static async Task SendToBotAsync()
        {
            var bot = new TelegramBotClient("1139402041:AAHbmIdDK1XaIXVhilFOgNF9uiAkHmj667c");
            var t = await bot.SendTextMessageAsync("-455430398", RecentMessage);
        }

    }
}
