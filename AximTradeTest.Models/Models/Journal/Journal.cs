using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Models.Models.Journal;

public class Journal : JournalInfo
{
    [Required]
    public string Text { get; set; }
}
