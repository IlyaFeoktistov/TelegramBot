namespace Telegram.Bot.Types
{
    internal class User
    {
        public long Id { get; set; }
        public bool IsBot { get; set; }
        public string FirstName { get; set; } = default!;
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? LanguageCode { get; set; }
        public bool? CanJoinGroups { get; set; }
    }
}
