using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace WebScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example of using WebScraper class
            while (true)
            {
                WebScraper webScraper = new WebScraper();

                Console.Write("Please enter the name of the person you are looking for >");
                string user_input = Console.ReadLine();
                webScraper.GetNews(user_input, 10); // user input and number of results 
                Thread.Sleep(2500);
                Console.Write("Please choose which article you would like to read >");
                int chosen_article = Convert.ToInt32(Console.ReadLine());
                webScraper.GetArticle(user_input, chosen_article); // user input and chosen article
                Console.ReadLine();

            }
        }
    }

    class Article
    {
        public string URL { get; set; }
        public string Title { get; set; }
    }

}

