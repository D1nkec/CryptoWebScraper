using HtmlAgilityPack; // Koristimo HtmlAgilityPack za parsiranje HTML-a
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebScraper
{
    public class CoinDeskSetup
    {
        // Metoda za dohvaćanje i ispisivanje cijena s Coindesk stranice
        public static async Task CoinDeskPrices()
        {
            // Definiramo niz URL-ova za Bitcoin i Ethereum
            string[] urls = {
                "https://www.coindesk.com/price/bitcoin/",
                "https://www.coindesk.com/price/ethereum/"
            };

            // Koristimo HttpClient za slanje HTTP zahtjeva
            using (var httpClient = new HttpClient())
            {
                // Prolazimo kroz svaki URL
                foreach (var url in urls)
                {
                    // Dohvaćamo cijenu valute s trenutnog URL-a
                    var price = await GetCurrencyPrice(httpClient, url);

                    // Ispisujemo cijenu valute
                    PrintPrice(GetCurrencyName(url), price);
                }
            }
        }

        // Metoda za dohvaćanje cijene valute s određenog URL-a
        static async Task<string> GetCurrencyPrice(HttpClient httpClient, string url)
        {
            try
            {
                // Dohvaćamo HTML s web stranice
                var html = await httpClient.GetStringAsync(url);
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);

                // Pronalazimo element koji sadrži cijenu
                var priceElement = htmlDoc.DocumentNode.SelectSingleNode("//span[contains(@class, 'currency-pricestyles__Price')]");

                // Vraćamo cijenu (ako je pronađena) nakon uklanjanja nepotrebnih praznina
                return priceElement?.InnerText.Trim();
            }
            catch (Exception ex)
            {
                // Ispisujemo grešku ako dođe do problema pri dohvaćanju cijene
                Console.WriteLine($"Error fetching price for {url}: {ex.Message}");
                return null;
            }
        }

        // Metoda za određivanje imena valute na temelju URL-a
        static string GetCurrencyName(string url)
        {
            // Provjeravamo sadrži li URL "bitcoin" ili "ethereum" i određujemo ime valute
            if (url.Contains("bitcoin"))
                return "BTC";
            else if (url.Contains("ethereum"))
                return "ETH";
            else
                return "Unknown";
        }

        // Metoda za ispisivanje cijene valute
        static void PrintPrice(string currencyName, string price)
        {
            // Ispisujemo cijenu valute ako je pronađena, inače ispisujemo poruku da cijena nije pronađena
            if (!string.IsNullOrEmpty(price))
            {
                Console.WriteLine($"Coindesk {currencyName} Price: {price}");
            }
            else
            {
                Console.WriteLine($"{currencyName} price not found.");
            }
        }
    }
}
