﻿using System;

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
            SQLiteConnection connection = new SQLiteConnection("Data Source=TempDatabase.db; Version=3;");
            connection.Open();

            // Create temporary schema
            string queryString;
            queryString = "create table PersonOfInterest(caseNo varchar(32), name varchar(64), dob DATETIME)";
            SQLiteCommand command;
            command = new SQLiteCommand(queryString, connection);
            command.ExecuteNonQuery();

            // Example new PoI
            queryString = "insert into PersonOfInterest (caseNo, name, dob) values ('case-1028293A', 'Joe Thomas', 1985-01-01)";

            command = new SQLiteCommand(queryString, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    class DatabaseWrapper
    {
        private SQLiteConnection connection;

        public DatabaseWrapper(string connectionString)
        {
            this.connection = new SQLiteConnection(connectionString);
        }

        static void NonQuery(string query)
        {
            this.connection.Open();
            var command = new SQLiteCommand(query, this.Connection);
            command.ExecuteNonQuery();
            this.connection.Close();
        }

        SQLiteDataReader ReaderQuery(string query)
        {
            this.connection.Open();
            var command = new SQLiteCommand(query, this.Connection);
            SQLiteDataReader reader = command.ExecuteReader();
            this.connection.Close();
            return reader;
        }

        public SQLiteConnection Connection
        {
            get { return this.connection; }
            set { this.connection = value; }
        }
    }
}
