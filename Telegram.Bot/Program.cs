using Telegram.Bot;
using Telegram.Bot.Commands;
using Telegram.Bot.Storage;
using Telegram.Bot.Types;

class Program
{
    private static TelegramBotClient? bot;
    private static StorageController? storage;

    private static CommandsController? commandsController;

    private static async Task Main()
    {
        var token = System.IO.File.ReadAllText("token");

        bot = new TelegramBotClient(token);
        storage = new StorageController("Metadata.json");
        commandsController = new CommandsController();

        bot.OnMessage += MessageListener;
        bot.OnDownloadFile += FileManager;

        storage.InitializeDocuments();
        await bot.StartReceiving();
        
        Console.ReadKey();
    }

    private static void MessageListener(object sender, Update update)
    {
        switch (update.Message?.Type)
        {
            case MessageType.Unknown:
                UnkownHandler(update);
                break;

            case MessageType.Text:
                TextHandler(update);
                break;

            case MessageType.Photo:
                PhotoHandlerAsync(update);
                break;

            case MessageType.Audio:
                AudioHandler(update);
                break;

            case MessageType.Video:
                VideoHandler(update);
                break;

            case MessageType.Voice:
                VoiceHandler(update);
                break;

            case MessageType.Document:
                DocumentHandlerAsync(update);
                break;

            case MessageType.Sticker:
                StickerHandler(update);
                break;
        };
    }

    private static void UnkownHandler(Update update)
    {
        throw new NotImplementedException();
    }

    private static void StickerHandler(Update update)
    {
        throw new NotImplementedException();
    }

    private static async void VoiceHandler(Update update)
    {
        var file = await bot!.GetFileAsync(update.Message.Voice.FileId.ToString());
        await bot.DownloadFileAsync(update.Message.Voice, file.FilePath);
    }

    private static void VideoHandler(Update update)
    {
        //var file = await bot!.GetFileAsync(update.Message.Video.FileId.ToString());
        //await bot.DownloadFileAsync(update.Message.Video, file.FilePath);
    }

    private static async void AudioHandler(Update update)
    {
        var file = await bot!.GetFileAsync(update.Message.Audio.FileId.ToString());
        await bot.DownloadFileAsync(update.Message.Audio, file.FilePath);
    }

    private static void TextHandler(Update update)
    {
        if (!commandsController.AlreadyExecuteCommand(update.Message.From.Id))
            commandsController.ExecuteCommand(bot!, update, storage!);
        else
            commandsController.ExecuteNextCommand(bot!, update, storage!);
    }

    private static void FileManager(object sender, Document document) => storage!.AddDocument(document);

    private static async void PhotoHandlerAsync(Update update)
    {
        foreach (Photo photo in update.Message.Photo)
        {
            var file = await bot!.GetFileAsync(photo.FileId.ToString());
            await bot.DownloadFileAsync(photo, file.FilePath);
        }
    }

    private static async void DocumentHandlerAsync(Update update)
    {
        var file = await bot!.GetFileAsync(update.Message.Document.FileId.ToString());
        await bot.DownloadFileAsync(update.Message.Document, file.FilePath);
    }
}