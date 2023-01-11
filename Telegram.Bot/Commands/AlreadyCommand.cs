namespace Telegram.Bot.Commands
{
    internal class AlreadyCommand
    {
        private int taskIndex;

        public AlreadyCommand(long userId, string commandName, int indexTask)
        {
            UserId = userId;
            CommandName = commandName;
            this.taskIndex = indexTask;
        }

        public long UserId { get; set; }
        public string? CommandName { get; set; }
        public int TaskIndex { get { return taskIndex; } }

        public void IndexIncrement() => taskIndex++;
    }
}
