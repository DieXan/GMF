using System.Net;
using System;
using static System.Net.Mime.MediaTypeNames;
using DatabaseHandler;
namespace ScrapHandler
{
    public class sf
    {
        db db = new db();
        public static void GetAllFoodInfo()
        {
            string htmlCode;
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://burgerking-menu-i-ceny.ru/");
            }
            string[] s1 = htmlCode.Split("<h2 class=\"text-center mt-4 mb-3 text-uppercase\">");
            for (int i = 1; i < s1.Length; i++)
            {
                string[] s2 = s1[i].Split("</h2>");
                string[] s3 = s1[i].Split("col-lg-3 col-md-3 col-sm-4 mt-4");
                Console.WriteLine(s2[0]);
                for (int j = 1; j < s3.Length - 1; j++)
                {
                    try
                    {
                        string[] s4 = s3[j].Split("Подробнее");
                        string[] s5 = s4[1].Split(">");
                        string[] s6 = s5[1].Split("<");
                        string[] s7 = s3[j].Split("Подробнее");
                        string[] s8 = s7[1].Split("р.</b>");
                        string[] s9 = s8[0].Split("> ");
                        Console.WriteLine("\t" + s6[0] + " - " + s9[s9.Length - 1] + " руб.");
                        try
                        {
                            if(s9[s9.Length - 1].Length > 2) { 
                                string name = Convert.ToString(s6[0]);
                                int p_rice = Convert.ToInt32(s9[s9.Length - 1]);
                                string category = Convert.ToString(s2[0]);
                                db.InsertFood(name, category, p_rice);
                            }
                        }
                        catch (InvalidCastException e)
                        {
                            Console.WriteLine(e);
                            throw;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    
                }
            }

        }
    }
}