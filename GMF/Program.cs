using System;
using Telegram.Bot;
using DatabaseHandler;
using System.Threading;
using Telegram.Bot.Types;
using ScrapHandler;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
class main
{

    static ITelegramBotClient bot = new TelegramBotClient("1925272046:AAF_kH6jfBY1B53L_EUTRhTBR_IsR1f5Ph0");
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message;
            if (message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Здравствуйте!");
                return;
            }
            await botClient.SendTextMessageAsync(message.Chat, "Дорогой, я ничего не понимаю!");
        }
    }
    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }
    static void Main()
    {
        //db db = new db();
        //ScrapFood sf = new ScrapFood();
        //sf.GetAllFoodInfo();
        //db.InsertFood("adsads", "asdasd", 342);
        //db.CreateDB();

        Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, // receive all update types
        };
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken
        );
        Console.ReadLine();
        Console.Read();
    }
}