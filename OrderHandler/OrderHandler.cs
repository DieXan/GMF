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
            string[] b = burger == 1 ? db.FindBurger(balance).Split(":") : "".Split();
            str += db.FindBurger(balance) + "\n";
            str += db.FindDrink(balance);
            return str;
        }
    }
}