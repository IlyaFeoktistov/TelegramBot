using Telegram.Bot.Types;
using Telegram.Bot.Storage;

namespace Telegram.Bot.Commands.Abstract
{
    internal interface ICommand
    {
        Enums.CommandType Type { get; }
        abstract bool Execute(TelegramBotClient bot, Update update, StorageController storage, int index);
    }
}
