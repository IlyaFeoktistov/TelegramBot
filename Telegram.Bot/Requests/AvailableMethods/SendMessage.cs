using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstract;

namespace Telegram.Bot.Requests.AvailableMethods
{
    internal class SendMessage : Request
    {
        [JsonProperty("chat_id")]
        public long ChatId { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("parse_mode")]
        public string ParseMode { get; set; }
        public SendMessage(long chatId, string text) : base("sendMessage") 
        {
            ChatId = chatId;
            Text = text;
            ParseMode = string.Empty;
        }
        public SendMessage(long chatId, string text, string parseMode) : base("sendMessage")
        {
            ChatId = chatId;
            Text = text;
            ParseMode = parseMode; //MarkdownV2
        }
    }
}
