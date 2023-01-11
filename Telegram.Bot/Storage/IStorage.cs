using Telegram.Bot.Types;

namespace Telegram.Bot.Storage
{
    internal interface IStorage
    {
        void InitializeDocuments();
        void AddDocument(Document document);
    }
}
