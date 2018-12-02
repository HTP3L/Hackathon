using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace Hack.Database
{
    public class DatabaseWrapper
    {
        private SQLiteConnection connection;

        public DatabaseWrapper(string connectionString)
        {
            Connection = new SQLiteConnection(connectionString);
            Connection.Open();
            var command = new SQLiteCommand("PRAGMA foreign_keys = ON;", Connection);
            command.ExecuteNonQuery();
            Connection.Close();
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

