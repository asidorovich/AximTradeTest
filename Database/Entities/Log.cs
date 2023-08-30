namespace Database.Entities;

public class Log : Entity
{
    public long EventId { get; set; }

    public string LogLevel { get; set; }

    public string Message { get; set; }

    public string Exception { get; set; }

    public string Path { get; set; }

    public string Data { get; set; }

    public string DataType { get; set; }

    public DateTime CreatedAt { get; set; }
}
