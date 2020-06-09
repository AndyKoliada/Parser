using System;
using System.Threading.Tasks;
using System.Timers;

namespace Parser
{
    class Program
    {
        public static void Main()
        {
            //MainAsync().GetAwaiter().GetResult();
            while (true)
            {
                Timer t = new Timer(60000);
                t.AutoReset = true;
                t.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                t.Start();

                async void OnTimedEvent(Object source, ElapsedEventArgs e)
                {
                    await Message.GetHtmlAsync();
                }
            }
        }

        //async void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{
        //    await Message.GetHtmlAsync();
        //}
        //private static async Task MainAsync()
        //{



        //    async void OnTimedEvent(Object source, ElapsedEventArgs e)
        //    {
        //        await Message.GetHtmlAsync();
        //    }
        //}

    }
}