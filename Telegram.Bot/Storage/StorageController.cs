using Newtonsoft.Json;
using Telegram.Bot.Types;

namespace Telegram.Bot.Storage
{
    internal class StorageController : IStorage
    {
        private List<Document> documents;

        public Document this [int index]
        {
            get => documents[index];
        }

        public StorageController(string fileName)
        {
            FileName = fileName;
            documents = new List<Document>();
        }

        public string FileName { get; private set; }

        public void InitializeDocuments()
        {
            string json = string.Empty;

            if (System.IO.File.Exists(FileName))
                json = System.IO.File.ReadAllText(FileName);
            else
                System.IO.File.Create(FileName);
            
            if(json != string.Empty)
                documents = JsonConvert.DeserializeObject<List<Document>>(json);
        }

        public void AddDocument(Document document)
        {
            var findedDocument = documents.Find(doc => doc.FileName == document.FileName);
            
            if(findedDocument is not null)
            {
                documents.Insert(documents.IndexOf(findedDocument), document);
            }
            else documents.Add(document);

            var json = JsonConvert.SerializeObject(documents);
            System.IO.File.WriteAllText(FileName, json);
        }

        public string GetDocumentId(string fileName)
        {
            var document = documents.Find(doc => doc.FileName!.ToLower() == fileName.ToLower());

            return (document != null) ? document.FileId! : string.Empty;
        }

        public override string ToString()
        {
            var stringDocuments = string.Empty;

            foreach (Document document in documents)
            {
                stringDocuments += $"{Path.GetDirectoryName(document.FilePath)} / {document.FileName}\n";
            }

            return stringDocuments;
        }
    }
}
