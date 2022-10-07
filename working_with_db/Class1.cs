using System;
using Microsoft.Data.Sqlite;
namespace working_with_db
{
    public class db
    {
        public void create_db()
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = "CREATE TABLE test(id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)";
                    command.ExecuteNonQuery();
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
                Console.WriteLine("База данных создана!");
            }
        }
        
    }
}