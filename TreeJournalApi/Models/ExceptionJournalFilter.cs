namespace TreeJournalApi.Models
{
    public class ExceptionJournalFilter
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Search { get; set; }
    }
}
