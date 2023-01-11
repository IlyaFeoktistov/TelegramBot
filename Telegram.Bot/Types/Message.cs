namespace Telegram.Bot.Types
{
    class Message
    {
        public int MessageId { get; set; }
        public MessageType Type =>
            this switch
            {
                { Text: { } } => MessageType.Text,
                { Photo: { } } => MessageType.Photo,
                { Audio: { } } => MessageType.Audio,
                //{ Video: { } } => MessageType.Video,
                { Voice: { } } => MessageType.Voice,
                { Document: { } } => MessageType.Document,
                //{ Sticker: { } } => MessageType.Sticker,
                _ => MessageType.Unknown
            };
        public From? From { get; set; }
        public Chat? Chat { get; set; }
        public string? Date { get; set; }
        public string? Text { get; set; }
        public Document? Document { get; set; }
        public Photo[]? Photo { get; set; }
        public Voice? Voice { get; set; }
        public Audio? Audio { get; set; }
    }
}
