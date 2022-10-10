using System;
using Microsoft.Data.Sqlite;
namespace DatabaseHandler
{
    public class db
    {
        public void CreateDB()
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = "CREATE TABLE users(id INTEGER NOT NULL, name TEXT NOT NULL)";
                    command.ExecuteNonQuery();
                    command.CommandText = "CREATE TABLE foods(id INTEGER NOT NULL, name TEXT NOT NULL, category TEXT NOT NULL, price INTEGER NOT NULL)";
                    command.ExecuteNonQuery();
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("База данных уже создана!");
                }
                Console.WriteLine("База данных создана!");
            }
        }
        public void InsertFood(string food, string category, int price)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM foods";
                    int id = Convert.ToInt16(command.ExecuteScalar().ToString());
                    command.CommandText = $"INSERT INTO foods(id, name, category, price) VALUES ({id}, '{food}', '{category}', '{price}')";
                    command.ExecuteNonQuery();
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public void InsertUser(int id_user)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = "SELECT COUNT(*) FROM foods";
                    int id = Convert.ToInt16(command.ExecuteScalar().ToString());
                    command.CommandText = $"INSERT INTO foods(id, name, category, price) VALUES ()";
                    command.ExecuteNonQuery();
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
    
}