namespace AximTradeTest.Models.Exceptions;

public class ExceptionModel
{
    public Guid Id { get; set; }

    public string Type { get; set; }

    public Dictionary<string, string> Data { get; set; } = new ();
}
