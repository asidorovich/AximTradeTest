using System.ComponentModel.DataAnnotations;

namespace AximTradeTest.Models.Models;

public class Range<T>
{
    [Required]
    public int Skip { get; set; }

    [Required]
    public int Top { get; set; }

    [Required]
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
}
