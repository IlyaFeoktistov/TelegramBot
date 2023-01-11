using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstract;

namespace Telegram.Bot.Requests.AvailableMethods
{
    internal class GetFile : Request
    {
        [JsonProperty("file_id")]
        public string? FileId { get; set; }
        public GetFile(string fileId) : base("getFile") { FileId = fileId; }
    }
}
