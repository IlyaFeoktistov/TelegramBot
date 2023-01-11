namespace Telegram.Bot.Types
{
    internal class Voice : File
    {
        public int Duration { get; set; }
        public string MimeType { get; set; }
    }
}
