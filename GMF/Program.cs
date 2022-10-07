using System;
using Microsoft.Data.Sqlite;
using working_with_db;
class main
{
    static void Main()
    {
        db db = new db();
        db.create_db();
        Console.Read();
    }
}