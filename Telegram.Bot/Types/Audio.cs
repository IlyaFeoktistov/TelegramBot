using Newtonsoft.Json;

namespace Telegram.Bot.Types
{
    internal class Audio : File
    {
        [JsonProperty("duration")]
        public int Duration { get; set; }
        [JsonProperty("performer")]
        public string Performer { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("file_name")]
        public string FileName { get; set; }
        [JsonProperty("mime_type")]
        public string MimeType { get; set; }
        [JsonProperty("thumb")]
        public Photo Thumb { get; set; }
    }
}
