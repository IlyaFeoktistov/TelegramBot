using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    internal class Update
    {
        [JsonProperty("update_id")]
        public int Id { get; set; }
        public Message? Message { get; set; }
    }
}
