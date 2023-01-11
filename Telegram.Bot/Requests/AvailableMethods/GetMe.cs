using Telegram.Bot.Requests.Abstract;

namespace Telegram.Bot.Requests.AvailableMethods
{
    internal class GetMe : Request
    {
        public GetMe() : base("getMe") {}
    }
}
