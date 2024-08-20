namespace TreeJournalApi.Models
{
    public class MJournal
    {
        public string? Text { get; set; } 
        public long Id { get; set; }    
        public long EventId { get; set; } 
        public string? CreatedAt { get; set; } 
    }
}
