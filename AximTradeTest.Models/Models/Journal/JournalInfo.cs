using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Models.Models.Journal;

public class JournalInfo
{
    [Required]
    public long Id { get; set; }

    [Required]
    public long EventId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}
