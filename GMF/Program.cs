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
using Microsoft.Data.Sqlite;

class main
{
    static Dictionary<int, int> UserStep = new Dictionary<int, int>();
    static ITelegramBotClient bot = new TelegramBotClient("1925272046:AAF_kH6jfBY1B53L_EUTRhTBR_IsR1f5Ph0");
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
        if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            ReplyKeyboardMarkup replyKeyboardMarkup_start = new(new[]
            {
                new KeyboardButton[] { "Задать параметры выбора", "Готовые комбо от нас", "О нас" },
            })
            {
                ResizeKeyboard = true
            };
            ReplyKeyboardMarkup replyKeyboardMarkup_YesOrNot = new(new[]
            {
                new KeyboardButton[] { "Да", "Нет" },
            })
            {
                ResizeKeyboard = true
            };
            ReplyKeyboardMarkup replyKeyboardMarkup_MoneyDef = new(new[]
            {
                new KeyboardButton[] { "250", "500", "750" },
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
            if (message.Text.ToLower() == "/db")
            {
                db.CreateDB();
                await botClient.SendTextMessageAsync(message.Chat, "Создаем бд!");
                return;
            }
            if (message.Text.ToLower() == "задать параметры выбора")
            {
                UserStep[Convert.ToInt32(message.Chat.Id)] = 1;
                await botClient.SendTextMessageAsync(message.Chat, "Ответьте на пару вопросов, пожалуйста.\nВаш заказ должен содержать выпить?да/нет", replyMarkup: replyKeyboardMarkup_YesOrNot);
                return;
            }
            if (UserStep.ContainsKey(Convert.ToInt32(message.Chat.Id)))
            {
                if (UserStep[Convert.ToInt32(message.Chat.Id)] == 1)
                {
                    if (message.Text.ToLower() == "да")
                    {
                        Console.WriteLine(message.Text.ToLower());
                        UO.ChangeDrinkOption(Convert.ToInt32(message.Chat.Id), 1);
                    }
                    else if ((message.Text.ToLower() == "нет"))
                    {
                        Console.WriteLine(message.Text.ToLower());
                        UO.ChangeDrinkOption(Convert.ToInt32(message.Chat.Id), 0);
                    }
                    UserStep[Convert.ToInt32(message.Chat.Id)] = 2;
                    await botClient.SendTextMessageAsync(message.Chat, "Ваш заказ должен содержать бургер?да/нет.", replyMarkup: replyKeyboardMarkup_YesOrNot);
                    return;
                }
                if (UserStep[Convert.ToInt32(message.Chat.Id)] == 2)
                {
                    if (message.Text.ToLower() == "да")
                    {
                        UO.ChangeBurgerOption(Convert.ToInt32(message.Chat.Id), 1);
                    }
                    else if ((message.Text.ToLower() == "нет"))
                    {
                        UO.ChangeBurgerOption(Convert.ToInt32(message.Chat.Id), 0);
                    }
                    UserStep[Convert.ToInt32(message.Chat.Id)] = 3;
                    await botClient.SendTextMessageAsync(message.Chat, "Ваш заказ должен содержать закуски?да/нет.", replyMarkup: replyKeyboardMarkup_YesOrNot);
                    return;
                }
                if (UserStep[Convert.ToInt32(message.Chat.Id)] == 3)
                {
                    if (message.Text.ToLower() == "да")
                    {
                        UO.ChangeEtcOption(Convert.ToInt32(message.Chat.Id), 1);
                    }
                    else if ((message.Text.ToLower() == "нет"))
                    {
                        UO.ChangeEtcOption(Convert.ToInt32(message.Chat.Id), 0);
                    }
                    UserStep[Convert.ToInt32(message.Chat.Id)] = 4;
                    UserStep.Remove(Convert.ToInt32(message.Chat.Id));
                    await botClient.SendTextMessageAsync(message.Chat, "Какой у вас бюджет?", replyMarkup: replyKeyboardMarkup_MoneyDef);
                    return;
                }
                if (UserStep[Convert.ToInt32(message.Chat.Id)] == 4)
                {
                    if (Convert.ToInt32(message.Text) >= 0)
                    {
                        UO.ChangeBalanceOption(Convert.ToInt32(message.Chat.Id), Convert.ToInt32(message.Text));
                    }
                    UserStep.Remove(Convert.ToInt32(message.Chat.Id));
                    await botClient.SendTextMessageAsync(message.Chat, "Готово!", replyMarkup: replyKeyboardMarkup_start);
                    return;
                }
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