using System.ComponentModel.DataAnnotations;

namespace TreeJournalApi.Models
{
    public class ExceptionJournal
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public long EventId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string QueryParameters { get; set; }
        public string BodyParameters { get; set; }

        [Required]
        public string StackTrace { get; set; }
    }
}
