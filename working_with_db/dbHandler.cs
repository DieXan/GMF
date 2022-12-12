using System;
using System.Windows.Input;
using Microsoft.Data.Sqlite;
namespace DatabaseHandler
{
    public class db
    {
        public static void CreateDB()
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
                    command.CommandText = "CREATE TABLE UsersOptions(id INTEGER NOT NULL, drink INTEGER NOT NULL, burger INTEGER NOT NULL, etc INTEGER NOT NULL, balance INTEGER NOT NULL, active INTEGER NOT NULL)";
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
        public static void InsertFood(string food, string category, int price)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    //command.CommandText = $"SELECT COUNT(*) FROM foods AS f WHERE f.name = {food} AND f.category = {category}";
                    command.CommandText = $"SELECT COUNT(*) FROM foods";
                    int id = Convert.ToInt16(command.ExecuteScalar().ToString());
                    command.CommandText = $"INSERT INTO foods(id, name, category, price) VALUES ({id}, '{food}', '{category}', '{price}')";
                    command.ExecuteNonQuery();
                }
                catch (InvalidCastException e)
                {
                    //Console.WriteLine(e);
                    throw;
                }
            }
        }
        public static int CheckActive(int id_user)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT COUNT(*) FROM UsersOptions WHERE id = {id_user}";
                    if (Convert.ToInt16(command.ExecuteScalar().ToString()) == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        command.CommandText = $"SELECT active FROM UsersOptions WHERE id = {id_user}";
                        if (Convert.ToInt16(command.ExecuteScalar().ToString()) == 0)
                        {
                            return 0;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    command.ExecuteNonQuery();
                }
                catch (InvalidCastException e)
                {
                    return 0;
                    throw;
                }
            }
        }
        public static string FindBurger(int balance)
        {
            string str = "";
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT id FROM foods WHERE (category = 'Бургеры из говядины' OR category = 'Бургеры из курицы и рыбы') AND price = (SELECT MIN(price) FROM foods WHERE category = 'Бургеры из говядины' OR category = 'Бургеры из курицы и рыбы')";
                    int FoodId = Convert.ToInt32(command.ExecuteScalar().ToString());
                    command.CommandText = $"SELECT name FROM foods WHERE id = {FoodId}";
                    str += command.ExecuteScalar().ToString() + ":";
                    command.CommandText = $"SELECT price FROM foods WHERE id = {FoodId}";
                    str += command.ExecuteScalar().ToString();
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
            return str;
        }
        public static string FindDrink(int balance)
        {
            string str = "";
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT id FROM foods WHERE category = 'Напитки' AND price = (SELECT MIN(price) FROM foods WHERE category = 'Напитки')";
                    int FoodId = Convert.ToInt32(command.ExecuteScalar().ToString());
                    command.CommandText = $"SELECT name FROM foods WHERE id = {FoodId}";
                    str += command.ExecuteScalar().ToString() + ":";
                    command.CommandText = $"SELECT price FROM foods WHERE id = {FoodId}";
                    str += command.ExecuteScalar().ToString();
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
            return str;
        }
        public static void InsertUser(int id_user, string name_user)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT COUNT(*) FROM users WHERE id = {id_user}";
                    if (Convert.ToInt16(command.ExecuteScalar().ToString()) == 0)
                    {
                        command.CommandText = $"INSERT INTO users(id, name) VALUES ('{id_user}', '{name_user}')";
                        command.ExecuteNonQuery();
                    }
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
    
}