using System;

using System.Collections.Generic;

using System.Data;
using System.Data.SQLite;

namespace Hack.DatabaseTemp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Temporarily create new db while we don't have one mapped out
            SQLiteConnection.CreateFile("TempDatabase.db");

            // Create connection
            var database = new DatabaseWrapper("Data Source=TempDatabase.db; Version=3;new=False;datetimeformat=CurrentCulture;");
            string queryString;

            // Create temporary schema
            queryString = "create table PersonOfInterest(caseNo varchar(32), name varchar(64), dob char(10))";
            database.NonQuery(queryString);

            // Example new PoI
            queryString = "insert into PersonOfInterest (caseNo, name, dob) values ('case-1028293A', 'Joe Thomas', '1985-01-01')";
            database.NonQuery(queryString);

            // Example SELECT
            queryString = "select * from PersonOfInterest;";
            var results = database.ReaderQuery(queryString);
            foreach (Dictionary<string, object> d in results)
            {
                DateTime v = DateTime.Parse((string)d["dob"]);
                Console.WriteLine(v.ToString("dd/MM/yyyy"));
            }
        }
    }

    public class DatabaseWrapper
    {
        private SQLiteConnection connection;

        public DatabaseWrapper(string connectionString)
        {
            Connection = new SQLiteConnection(connectionString);
        }

        // Execute a query which doesn't return values
        public void NonQuery(string query)
        {
            Connection.Open();
            var command = new SQLiteCommand(query, Connection);
            command.ExecuteNonQuery();
            Connection.Close();
        }

        // Execute a query that returns values as a list of dictionaries
        public List<Dictionary<string, object>> ReaderQuery(string query)
        {
            Connection.Open();
            var command = new SQLiteCommand(query, Connection);
            SQLiteDataReader reader = command.ExecuteReader();

            var queryResults = new List<Dictionary<string, object>>();
            var columnNames = new List<string>();

            // Iterate over column names
            for (var i = 0; i < reader.FieldCount; i++)
                columnNames.Add(reader.GetName(i));
            // For each row, serialize as a dict
            while (reader.Read())
                queryResults.Add(SerializeRow(columnNames, reader));

            Connection.Close();
            return queryResults;
        }

        private Dictionary<string, object> SerializeRow(IEnumerable<string> cols, SQLiteDataReader reader) {
            var result = new Dictionary<string, object>();
            foreach (var col in cols)
                result.Add(col, reader[col]);
            return result;
        }

        public SQLiteConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
    }
}
