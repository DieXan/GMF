using System;
using Microsoft.Data.Sqlite;
namespace UserOption
{
    public class UO
    {
        
        /*public static void ChangeStep(int user_id, int step)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT COUNT(*) FROM UsersOptions WHERE id = {user_id}";
                    if (Convert.ToInt16(command.ExecuteScalar().ToString()) == 0)
                    {
                        command.CommandText = $"INSERT INTO UsersOptions(id, step, drink, burger) VALUES ('{user_id}', 1, 0, 0)";
                        command.ExecuteNonQuery();
                    }
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
        }*/
        public static void ChangeDrinkOption(int user_id, int NeedOrNot)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT COUNT(*) FROM UsersOptions WHERE id = {user_id}";
                    if (Convert.ToInt16(command.ExecuteScalar().ToString()) == 0)
                    {
                        command.CommandText = $"INSERT INTO UsersOptions(id, drink, burger, etc, balance, active) VALUES ('{user_id}', {NeedOrNot}, 666, 666, 666, 0)";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = $"UPDATE UsersOptions SET drink = {NeedOrNot} WHERE id = {user_id}";
                        command.ExecuteNonQuery();
                    }
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public static void ChangeBurgerOption(int user_id, int NeedOrNot)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT COUNT(*) FROM UsersOptions WHERE id = {user_id}";
                    if (Convert.ToInt16(command.ExecuteScalar().ToString()) == 1)
                    {
                        command.CommandText = $"UPDATE UsersOptions SET burger = {NeedOrNot} WHERE id = {user_id}";
                        command.ExecuteNonQuery();
                    }
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public static void ChangeEtcOption(int user_id, int NeedOrNot)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT COUNT(*) FROM UsersOptions WHERE id = {user_id}";
                    if (Convert.ToInt16(command.ExecuteScalar().ToString()) == 1)
                    {
                        command.CommandText = $"UPDATE UsersOptions SET etc = {NeedOrNot} WHERE id = {user_id}";
                        command.ExecuteNonQuery();
                    }
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public static void ChangeBalanceOption(int user_id, int Balance)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT COUNT(*) FROM UsersOptions WHERE id = {user_id}";
                    if (Convert.ToInt16(command.ExecuteScalar().ToString()) == 1)
                    {
                        command.CommandText = $"UPDATE UsersOptions SET balance = {Balance} WHERE id = {user_id}";
                        command.ExecuteNonQuery();
                        command.CommandText = $"UPDATE UsersOptions SET active = 1 WHERE id = {user_id}";
                        command.ExecuteNonQuery();
                    }
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                }
            }
        }
        public static string GetUserOption(int user_id)
        {
            using (var connection = new SqliteConnection("Data Source=global.db"))
            {
                connection.Open();
                try
                {
                    string str = "";
                    SqliteCommand command = new SqliteCommand();
                    command.Connection = connection;
                    command.CommandText = $"SELECT drink FROM UsersOptions WHERE id = {user_id}";
                    str += command.ExecuteScalar().ToString() + ":";
                    command.CommandText = $"SELECT burger FROM UsersOptions WHERE id = {user_id}";
                    str += command.ExecuteScalar().ToString() + ":";
                    command.CommandText = $"SELECT etc FROM UsersOptions WHERE id = {user_id}";
                    str += command.ExecuteScalar().ToString() + ":";
                    command.CommandText = $"SELECT balance FROM UsersOptions WHERE id = {user_id}";
                    str += command.ExecuteScalar().ToString();
                    return str;
                }
                catch (InvalidCastException e)
                {
                    Console.WriteLine(e);
                    return "";
                }
            }
        }
    }
}