using System;
using System.Threading;

namespace Parser
{
    class CallTimer
    {
        internal static void Run()
        {
            int seconds = 60 * 1000;

            var timer =
                new Timer(TimerMethod, null, 0, seconds);

            Console.ReadKey();
        }

        private async static void TimerMethod(object o)
        {
            Console.WriteLine(
                "Running: " + DateTime.Now);
            await Message.GetHtmlAsync();
        }
    }
}
