using Telegram.Bot.Commands.Abstract;
using Telegram.Bot.Commands.Enums;
using Telegram.Bot.Storage;
using Telegram.Bot.Types;

namespace Telegram.Bot.Commands
{
    internal class CommandsController
    {
        private readonly List<AlreadyCommand>? alreadyCommands;
        private readonly InitializeCommands? commandsContainer;

        public CommandsController()
        {
            alreadyCommands = new List<AlreadyCommand>();
            commandsContainer = new InitializeCommands();
        }

        /// <summary>
        /// Позволяет узнать, выполняется ли какая-то комманда
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool AlreadyExecuteCommand(long userId)
        {
            if(alreadyCommands is not null)
                return alreadyCommands.Find(obj => obj.UserId == userId) != null;

            return false;
        }

        public void ExecuteCommand(TelegramBotClient bot, Update update, StorageController storage)
        {
            string commandText = update!.Message!.Text!.ToLower();
            long userId = update!.Message!.From!.Id;

            if (commandsContainer!.Commands.TryGetValue(commandText, out ICommand? command))
            {
                command.Execute(bot!, update, storage!, 0);

                if (command.Type == CommandType.Loop)
                    alreadyCommands!.Add(new AlreadyCommand(userId, commandText, 1));
            }
        }

        /// <summary>
        /// Для многошаговых комманд
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="update"></param>
        /// <param name="storage"></param>
        public void ExecuteNextCommand(TelegramBotClient bot, Update update, StorageController storage)
        {
            long userId = update!.Message!.From!.Id;
            AlreadyCommand alreadyCommand = alreadyCommands.Find(obj => obj.UserId == userId);

            if (commandsContainer!.Commands.TryGetValue(alreadyCommand.CommandName, out ICommand? command))
            {
                if (command.Execute(bot!, update, storage!, alreadyCommand.TaskIndex))
                {
                    alreadyCommands!.RemoveAt(
                        alreadyCommands.IndexOf(alreadyCommand));
                }
                else
                {
                    alreadyCommand.IndexIncrement();
                }
            }
        }
    }
}
