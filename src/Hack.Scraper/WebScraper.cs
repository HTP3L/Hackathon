using System;
using System.Collections.Generic;
using System.Text;
using HtmlAgilityPack;
using System.Linq;
using System.Net.Http;

namespace WebScraper
{
    class WebScraper
    {
        public Article[] Articles;

        public string ArticleText;

        public async void GetNews(string user_input, int ResultNum)
        {
            // Getting news from DailyMail
            user_input.Replace(@" ", "+");
            var url = "https://www.dailymail.co.uk/home/search.html?sel=site&size=" + ResultNum + "&searchPhrase=" + user_input + "&sort=relevant";
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            string title, current_url;
            htmlDocument.LoadHtml(html);
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("sch-res-content")).ToList();
            Article[] article = new Article[10];
            for (int i = 0; i < ResultNum; i++)
            {
                var ProductListItems = ProductsHtml[i].Descendants("h3").Where(node => node.GetAttributeValue("class", "").Contains("sch-res-title")).ToList();
                foreach (var ProductListItem in ProductListItems)
                {
                    title = System.Net.WebUtility.HtmlDecode(ProductListItem.InnerText);
                    current_url = "https://www.dailymail.co.uk" + ProductListItem.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                    article[i] = new Article();
                    article[i].Title = title;
                    article[i].URL = current_url;
                    //Console.WriteLine(i + 1 + ". " + title);
                    //Console.WriteLine("Link: " + current_url);
                }
            }

            Articles = article;
        }

        public async void GetArticle(string user_input, int chosen_article)
        {
            ArticleText = "";
            // Getting individual article from DailyMail
            user_input.Replace(@" ", "+");
            var url = "https://www.dailymail.co.uk/home/search.html?sel=site&size=" + chosen_article + "&searchPhrase=" + user_input + "&sort=relevant";
            var httpclient = new HttpClient();
            var html = await httpclient.GetStringAsync(url);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var ProductsHtml = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").Equals("sch-res-content")).ToList();
                    var ProductListItemsSpan = ProductsHtml[chosen_article-1].Descendants("span").Where(node => node.GetAttributeValue("class", "").Contains("ccow icn-text")).ToList();
                    foreach (var ProductListItem in ProductListItemsSpan)
                    {
                        if (ProductListItem.InnerText == "Video")
                        {
                            Console.WriteLine("Sorry, your content is a video which can't be played in the console.");
                        }
                        else
                        {
                            var ProductListItems = ProductsHtml[chosen_article-1].Descendants("h3").Where(node => node.GetAttributeValue("class", "").Contains("sch-res-title")).ToList();
                            foreach (var ProductListItem2 in ProductListItems)
                            {
                                Console.WriteLine();
                                Console.WriteLine("================== " + System.Net.WebUtility.HtmlDecode(ProductListItem2.InnerText) + " ==================");
                                Console.WriteLine();
                                url = "https://www.dailymail.co.uk" + ProductListItem2.Descendants("a").FirstOrDefault().GetAttributeValue("href", "");
                                html = await httpclient.GetStringAsync(url);
                                htmlDocument = new HtmlDocument();
                                htmlDocument.LoadHtml(html);
                                ProductsHtml = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("itemprop", "").Equals("articleBody")).ToList();
                                ProductListItems = ProductsHtml[0].Descendants("p").ToList();
                                foreach (var Paragraph in ProductListItems)
                                {
                                    ArticleText += Paragraph.InnerText;
                                }
                                break;
                            }
                        }
                        break;
                    }
        }
    }
}
