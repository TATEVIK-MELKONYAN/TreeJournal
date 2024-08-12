namespace TreeJournalApi.Models
{
    public class ExceptionJournal
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string QueryParameters { get; set; }
        public string BodyParameters { get; set; }
        public string StackTrace { get; set; }
    }
}
