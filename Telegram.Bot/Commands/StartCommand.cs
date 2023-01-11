using Telegram.Bot.Commands.Abstract;
using Telegram.Bot.Storage;
using Telegram.Bot.Types;

namespace Telegram.Bot.Commands
{
    internal class StartCommand : CommandBase
    {
        private const string COMMANDS = $"/start" +
            $"\n/get_files_list" +
            $"\n/get_file";
        public StartCommand(ref Dictionary<string, ICommand> commands)
            : base("/start", ref commands) { }

        public override bool Execute(TelegramBotClient bot, Update update, StorageController storage, int index)
        {
            bot?.SendMessage(update!.Message!.Chat!.Id, COMMANDS);
            return true;
        }
    }
}
