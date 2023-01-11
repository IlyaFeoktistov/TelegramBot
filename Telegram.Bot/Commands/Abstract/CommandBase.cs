using Telegram.Bot.Storage;
using Telegram.Bot.Types;
using Telegram.Bot.Commands.Enums;

namespace Telegram.Bot.Commands.Abstract
{
    internal class CommandBase : ICommand
    {
        public CommandBase(string commandName, ref Dictionary<string, ICommand> commands) 
            : this(commandName, CommandType.Default, ref commands) { }

        public CommandBase(string commandName, CommandType type, ref Dictionary<string, ICommand> commands)
        {
            CommandName = commandName;
            Type = type;
            commands.Add(commandName, this);
        }

        public string CommandName { get; private set; }
        public CommandType Type { get; private set; }

        public virtual bool Execute(TelegramBotClient bot, Update update, StorageController storage, int index)
        {
            Console.WriteLine($"Вызов {this}");
            bot?.SendMessage(update!.Message!.Chat!.Id, $"Вызов {this}");
            return true;
        }
    }
}
