namespace Telegram.Bot.Commands.Abstract
{
    internal class InitializeCommands
    {
        public Dictionary<string, ICommand> Commands;

        public InitializeCommands()
        {
            Commands = new Dictionary<string, ICommand>();
            
            _ = new StartCommand(ref Commands);
            _ = new GetFilesCommand(ref Commands);
            _ = new GetFileCommand(ref Commands);
        }
    }
}
