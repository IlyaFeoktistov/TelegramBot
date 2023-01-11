namespace Telegram.Bot.Types
{
    internal class From
    {
        public long Id { get; set; }
        public bool IsBot { get; set; }
        public string? FirstName { get; set; }
        public string? Username { get; set; }
        public string? LanguageCode { get; set; }
    }
}
