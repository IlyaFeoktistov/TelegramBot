using Telegram.Bot.Commands.Abstract;
using Telegram.Bot.Storage;
using Telegram.Bot.Types;

namespace Telegram.Bot.Commands
{
    internal class GetFileCommand : CommandBase
    {
        public GetFileCommand(ref Dictionary<string, ICommand> commands)
            : base("/get_file", Enums.CommandType.Loop, ref commands) { }

        public override bool Execute(TelegramBotClient bot, Update update, StorageController storage, int index)
        {
            var chatId = update.Message.Chat.Id;
            var fileName = update.Message.Text;

            switch (index)
            {
                default:
                case 0:
                    bot?.SendMessage(chatId, "Введите название файла:");
                    return false;

                case 1:
                    bot?.SendFile(chatId, storage.GetDocumentId(fileName));
                    return true;
            }
        }
    }
}
