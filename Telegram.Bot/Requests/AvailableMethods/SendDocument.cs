using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstract;

namespace Telegram.Bot.Requests.AvailableMethods
{
    internal class SendDocument : Request
    {
        [JsonProperty("chat_id")]
        public int ChatId { get; set; }
        [JsonProperty("document")]
        public string Document { get; set; }
        public SendDocument(int chatId, string document) : base("sendDocument") 
        {
            ChatId = chatId;
            Document = document;
        }
    }
}