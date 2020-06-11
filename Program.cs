using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        public static void Main()
        {
            MainAsync().GetAwaiter().GetResult();
            CallTimer.Run();
        }

        private static async Task MainAsync()
        {
            await Message.GetHtmlInitAsync();
        }

    }
}