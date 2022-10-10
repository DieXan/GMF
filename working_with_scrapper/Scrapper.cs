using System.Net;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace ScrapHandler
{
    public class ScrapFood
    {
        public void GetAllFoodInfo()
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
                string[] s3 = s2[i].Split("col-lg-3 col-md-3 col-sm-4 mt-4");
                Console.WriteLine(s2[0]);
                for (int j = 1; j < s3.Length - 1; j++)
                {
                    try
                    {
                        string[] s4 = s3[j].Split("Подробнее");
                        string[] s5 = s4[1].Split(">");
                        string[] s6 = s5[1].Split("<");
                        Console.WriteLine(s6[0]);
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