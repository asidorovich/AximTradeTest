using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Models.Models.Journal;

public class JournalInfoData
{
    [Required]
    public long Id { get; set; }

    [Required]
    public string? EventId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }
}
