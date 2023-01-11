using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    internal class Document : File
    {
        [JsonProperty("file_name")]
        public string? FileName { get; set; }
        [JsonProperty("mime_type")]
        public string? MimeType { get; set; }
    }
}
