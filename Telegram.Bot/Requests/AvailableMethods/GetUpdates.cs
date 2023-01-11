using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstract;

namespace Telegram.Bot.Requests.AvailableMethods
{
    internal class GetUpdates : Request
    {
        [JsonProperty("offset")]
        public int? Offset { get; set; }
        [JsonProperty("limit")]
        public int? Limit { get; set; }

        public GetUpdates() : base("getUpdates") { }
    }
}
