namespace Telegram.Bot.Types
{
    internal class ApiResponse<TResult>
    {
        public ApiResponse(bool ok, TResult result)
        {
            Ok = ok;
            Result = result;
        }

        public bool Ok { get; private set; }
        public TResult Result { get; private set; }
    }
}
