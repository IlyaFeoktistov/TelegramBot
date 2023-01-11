using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    internal class Chat
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string Type { get; set; }
    }
}
