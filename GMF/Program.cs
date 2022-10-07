using System;
using working_with_db;
using working_with_scrapper;
class main
{
    static void Main()
    {
        //db db = new db();
        //db.create_db();
        first first = new first();
        first.get_one();
        Console.Read();
    }
}