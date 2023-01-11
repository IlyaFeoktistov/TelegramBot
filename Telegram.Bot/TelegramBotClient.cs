using Newtonsoft.Json;
using Telegram.Bot.Requests.Abstract;
using Telegram.Bot.Requests.AvailableMethods;
using Telegram.Bot.Types;

namespace Telegram.Bot
{
    internal class TelegramBotClient
    {
        private const string BASE_TELEGRAM_URL = "https://api.telegram.org";

        private readonly string _defaultBaseUrl;
        private readonly string _fileBaseUrl;
        private readonly HttpClient _httpClient;

        public TelegramBotClient(string token)
        {
            _defaultBaseUrl = $"{BASE_TELEGRAM_URL}/bot{token}";
            _fileBaseUrl = $"{BASE_TELEGRAM_URL}/file/bot{token}";
            _httpClient = new HttpClient();
        }

        public delegate void MessageHandler(object sender, Update update);
        public event MessageHandler? OnMessage;

        public delegate void FileHandler(object sender, Document document);
        public event FileHandler? OnDownloadFile;

        /// <summary>
        /// При получении сообщения, вызывает событие OnMessage 
        /// </summary>
        /// <returns></returns>
        public async Task StartReceiving()
        {
            GetUpdates request = new GetUpdates() { Offset = 0 };
            while (true)
            {
                var updates = await MakeRequestAsync<Update[]>(request);
                foreach (dynamic update in updates)
                {
                    request.Offset = update.Id + 1;
                    OnMessage(this, update);
                }
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Создает и отправляет запрос через API
        /// </summary>
        /// <typeparam name="TResponse">Тип возвращаемого результата</typeparam>
        /// <param name="request">Запрос</param>
        /// <returns></returns>
        private async Task<TResponse> MakeRequestAsync<TResponse>(IRequest request)
        {
            string url = $"{_defaultBaseUrl}/{request.MethodName}";

            return await SendRequestAsync<TResponse>(request, url);
        }

        private async Task<TResponse> SendRequestAsync<TResponse>(IRequest request, string url)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(request.Method, url)
            {
                Content = request.ToHttpContent()
            };

            HttpResponseMessage httpResponse = await _httpClient.SendAsync(httpRequest);

            var json = await httpResponse.Content.ReadAsStringAsync();

            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TResponse>>(json);

            if (request.MethodName != "getUpdates")
                Console.WriteLine($"Был выполнен метод {request.MethodName}");

            return apiResponse.Result!;
        }

        /// <summary>
        /// Подготавливает файл к загрузке
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<Types.File> GetFileAsync(string fileId) =>
            await MakeRequestAsync<Types.File>(new GetFile(fileId));

        /// <summary>
        /// Загружает файл на сервер
        /// </summary>
        /// <param name="fileName">Имя создаваемого файла</param>
        /// <param name="path">Путь к файлу на сервере telegram</param>
        /// <param name="localFileName">Имя нового файла (локального)</param>
        /// <returns></returns>
        public async Task DownloadFileAsync<TFile>(TFile file, string path)
        {
            string url = $"{_fileBaseUrl}/{path}";
            string fileName;
            string directory = GetDirectory(path);


            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (file.GetType() == typeof(Document))
            {
                Document? document = file as Document;
                fileName = $"{directory}/{document.FileName}";
            }
            else fileName = path;


            Types.File tempFile = file as Types.File;

            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using Stream contentStream = (await _httpClient.SendAsync(request)).Content.ReadAsStream(),
                  stream = new FileStream(fileName, FileMode.Create);

            await contentStream.CopyToAsync(stream);
            OnDownloadFile(this, new Document()
            {
                FileName = Path.GetFileName(fileName),
                FileId = tempFile.FileId,
                FilePath = fileName,
                FileSize = tempFile.FileSize,
                FileUniqueId = tempFile.FileUniqueId
            });
        }

        private static string GetDirectory(string path)
        {
            var directory = Path.GetDirectoryName(path);
            
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            return directory;
        }

        /// <summary>
        /// Отправляет сообщение
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task SendMessage(int chatId, string text) =>
            await MakeRequestAsync<Message>(new SendMessage(chatId, text));

        public async Task SendFile(int chatId, string fileId) =>
            await MakeRequestAsync<Message>(new SendDocument(chatId, fileId));
    }
}
