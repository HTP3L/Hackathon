using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SQLite;

using Hack.Database;

namespace PopulateDB
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create connection
            var database = new DatabaseWrapper("Data Source=RinalDatabase.db; Version=3;new=False;datetimeformat=CurrentCulture;");
            // Example SELECT
            string queryString = "select report_text, filed_date, contact_id from Report r inner join PoliceCase c on r.case_id = c.id;";
            // string queryString = "insert into NewsArticles (headline, url) values (USE STRING FORMATTING TO ADD THE HEADLINE AND URL HERE);";
            var results = database.ReaderQuery(queryString);
            foreach (Dictionary<string, object> d in results)
            {
                string v = (string)d["report_text"];
                Console.WriteLine(v);
            }
        }
    }
}
