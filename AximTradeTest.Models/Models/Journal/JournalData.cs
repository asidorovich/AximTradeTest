using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Models.Models.Journal;

public class JournalData : JournalInfoData
{
    [Required]
    public string Text { get; set; }
}
