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
            var bot = new TelegramBotClient("YOUR_API_KEY");
            var t = await bot.SendTextMessageAsync("YOUR_CHAT_ID", RecentMessage);
        }

    }
}
