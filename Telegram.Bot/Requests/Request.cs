using Newtonsoft.Json;
using System.Text;

namespace Telegram.Bot.Requests.Abstract
{
    internal class Request : IRequest
    {
        public Request(string methodName)
        {
            MethodName = methodName;
            Method = HttpMethod.Post;
        }

        public Request(string methodName, HttpMethod httpMethod)
        {
            MethodName = methodName;
            Method = httpMethod;
        }

        [JsonIgnore]
        public HttpMethod Method { get; set; }

        [JsonIgnore]
        public string MethodName { get; set; }

        public HttpContent? ToHttpContent()
        {
            string payload = JsonConvert.SerializeObject(this);
            
            return new StringContent(
                content: payload,
                encoding: Encoding.UTF8,
                mediaType: "application/json"
            );
        }
    }
}
