using System;
using Telegram.Bot;
using DatabaseHandler;
using System.Threading;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using ScrapHandler;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using UserOption;

class main
{
    db db = new db();
    sf sf = new sf();
    UO UO = new UO();
    static ITelegramBotClient bot = new TelegramBotClient("1925272046:AAF_kH6jfBY1B53L_EUTRhTBR_IsR1f5Ph0");
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup_start = new(new[]
            {
                new KeyboardButton[] { "Задать параметры", "Готовые комбо от нас", "О нас" },
            })
            {
                ResizeKeyboard = true
            };
            var message = update.Message;
            if (message.Text.ToLower() == "/start")
            {
                db.InsertUser(Convert.ToInt32(message.Chat.Id), message.Chat.FirstName);
                await botClient.SendTextMessageAsync(message.Chat, "Здравствуйте!", replyMarkup: replyKeyboardMarkup_start);
                return;
            }
            if (message.Text.ToLower() == "о нас")
            {
                await botClient.SendTextMessageAsync(message.Chat, "Над созданием экосистемы виновны:\n@cute_admin\n@bulletproof_heart", replyMarkup: replyKeyboardMarkup_start);
                return;
            }
            if (message.Text.ToLower() == "/update")
            {
                sf.GetAllFoodInfo();
                await botClient.SendTextMessageAsync(message.Chat, "Проверка на новые позиции началась!");
                return;
            }
            if (message.Text.ToLower() == "Настроить параметры вывода")
            {
                UO.ChangeStep(Convert.ToInt32(message.Chat.Id), 1);
                await botClient.SendTextMessageAsync(message.Chat, "Ответьте на пару вопросов, пожалуйста.");
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