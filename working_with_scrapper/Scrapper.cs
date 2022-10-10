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
                Console.WriteLine(s2[0]);
            }

        }
    }
}