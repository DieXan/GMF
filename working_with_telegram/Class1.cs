using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

namespace working_with_telegram
{
    public class telegram_hehe
    {
        static ITelegramBotClient bot = new TelegramBotClient("TOKEN");
        public void send_message()
        {
            botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
        }
    }
}