namespace AximTradeTest.Models.Exceptions;

public class SecurityException : Exception
{
    public object Data { get; set; }

    public string Name { get; set; } = "Secure";

    public SecurityException()
        : base()
    {
    }

    public SecurityException(string message)
        : base(message)
    {
    }

    public SecurityException(string message, object data)
        : base(message)
    {
        Data = data;
    }
}
