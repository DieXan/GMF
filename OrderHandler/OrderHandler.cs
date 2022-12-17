using UserOption;
using DatabaseHandler;
namespace OrderHandler
{
    public class OH
    {
        public static string GetFoodForUser(int UserID)
        {
            string str = "Ваш заказ будет составать из:\n";
            string ss = UO.GetUserOption(UserID);
            string[] s = ss.Split(":");
            int drink = Convert.ToInt32(s[0]);
            int burger = Convert.ToInt32(s[1]);
            int etc = Convert.ToInt32(s[2]);
            int balance = Convert.ToInt32(s[3]);
            if (burger == 1) 
            {
                string[] tmp = db.FindBurger(balance).Split(":");
                str += $"\nНазвание позиции: {tmp[0]}\nЦена: {tmp[1]}";
            }
            if (drink == 1)
            {
                string[] tmp = db.FindDrink(balance).Split(":");
                str += $"\nНазвание позиции: {tmp[0]}\nЦена: {tmp[1]}";
            }
            if (etc == 1)
            {
                string[] tmp = db.FindEtc(balance).Split(":");
                str += $"\nНазвание позиции: {tmp[0]}\nЦена: {tmp[1]}";
            }
            return str;
        }
    }
}