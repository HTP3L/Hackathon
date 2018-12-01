using System;

using System.Data;
using System.Data.SQLite;

namespace Hack.DatabaseTemp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(databaseInfo))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = query;
                command.CommandTimeout = 15;
                command.CommandType = CommandType.Text;
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    reader.Read();
                }
                finally
                {
                    reader.Close();
                }
            }
        }
    }
}
