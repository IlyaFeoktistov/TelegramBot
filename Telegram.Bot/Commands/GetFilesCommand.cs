using Telegram.Bot.Commands.Abstract;
using Telegram.Bot.Storage;
using Telegram.Bot.Types;

namespace Telegram.Bot.Commands
{
    internal class GetFilesCommand : CommandBase
    {
        public GetFilesCommand(ref Dictionary<string, ICommand> commands)
            : base("/get_files_list", ref commands) { }

        public override bool Execute(TelegramBotClient bot, Update update, StorageController storage, int index)
        {
            bot.SendMessage(update.Message.Chat.Id, storage.ToString());
            return true;
        }
    }
}
