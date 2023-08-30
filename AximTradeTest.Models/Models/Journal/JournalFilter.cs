using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Models.Models.Journal;

public class JournalFilter
{
    [Required]
    [BindProperty(Name = "search")]
    public string Search { get; set; }

    [BindProperty(Name = "from")]
    public DateTime? From { get; set; }

    [BindProperty(Name = "to")]
    public DateTime? To { get; set; }
}
