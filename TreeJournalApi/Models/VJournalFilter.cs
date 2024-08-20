namespace TreeJournalApi.Models
{
    public class VJournalFilter
    {
        public string? From { get; set; }  
        public string? To { get; set; } 
        public string? Search { get; set; }
        public int Skip { get; set; }   
        public int Count { get; set; }
    }
}
