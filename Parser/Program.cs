using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using AngleSharp.Text;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Windows;

namespace Parser
{ 
    class Program
    {
        static void Main(string[] args)
        {
            GetHtmlAsync();

            Console.ReadLine();
        }

        private static async void GetHtmlAsync()
        {
            string url = "https://dumskaya.net/user/neravnoduschnyj/";

            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var htmlDocument = new HtmlDocument();

            htmlDocument.LoadHtml(html);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding = Encoding.GetEncoding("windows-1251");


            Console.WriteLine(html.Result);
        }
    }
}