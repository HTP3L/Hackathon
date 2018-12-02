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
            var database = new DatabaseWrapper("Data Source=FinalDatabase.db; Version=3;new=False;datetimeformat=CurrentCulture;");
            string queryString;

            // Example new PoI
            queryString = "insert into PoliceCase (case_type, start_date, end_date) values ('missing', '2018-01-01', '2018-01-05')";
            database.NonQuery(queryString);
            queryString = "insert into PoliceCase (case_type, start_date) values ('missing', '2018-01-01')";
            database.NonQuery(queryString);
            queryString = "insert into PoliceCase (case_type, start_date) values ('missing', '2018-01-01')";
            database.NonQuery(queryString);

            database.NonQuery("insert into Address (line1, town, county, postcode) values ('1 Hack The Street', 'Lincoln', 'Lincolnshire', 'LN6 1337')");

            database.NonQuery("insert into PersonOfInterest (name, date_of_birth, twitter_account, address_id, case_id) values ('John Doe', '1986-07-01', 'https://twitter.com/mrjohnisnothere', 0, 0);");
            database.NonQuery("insert into PersonOfInterest (name, date_of_birth, address_id, case_id) values ('John Poe', '1986-07-01', 0, 1);");
            database.NonQuery("insert into PersonOfInterest (name, date_of_birth, address_id, case_id) values ('John Snoe', '1986-07-01', 0, 2);");

            database.NonQuery("insert into Contact (phone_number, email_address, address_id) values ('07152595758', 'jonathonwoe@htp3.uk', 0);");

            database.NonQuery("insert into Report (report_text, filed_date, case_id, contact_id) values ('Big report', '2018-02-01', 2, 0);");

            // Example SELECT
            queryString = "select * from PersonOfInterest;";
            var results = database.ReaderQuery(queryString);
            foreach (Dictionary<string, object> d in results)
            {
                DateTime v = DateTime.Parse((string)d["date_of_birth"]);
                Console.WriteLine(v.ToString("dd/MM/yyyy"));
            }
        }
    }
}
