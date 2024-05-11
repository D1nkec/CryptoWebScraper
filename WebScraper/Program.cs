using HtmlAgilityPack;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace WebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("*****Coindesk Prices*****");
            Console.ResetColor(); 
            await CoinDeskSetup.CoinDeskPrices();
        }
    }
}
