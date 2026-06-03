using System.Runtime.Serialization;

namespace Practice.Exceptions;

[Serializable]
public class DatabaseException : Exception, ISerializable
{
    public string Query { get; private set; } = string.Empty;
    public string DatabaseName { get; private set; } = string.Empty;

    public DatabaseException()
        : base()
    {
    }

    public DatabaseException(string message)
        : base(message)
    {
    }

    public DatabaseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public DatabaseException(string message, string databaseName, string query)
        : base(message)
    {
        DatabaseName = databaseName;
        Query = query;
    }

    public DatabaseException(string message, string databaseName, string query, Exception innerException)
        : base(message, innerException)
    {
        DatabaseName = databaseName;
        Query = query;
    }

    protected DatabaseException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
        Query = info.GetString("Query") ?? string.Empty;
        DatabaseName = info.GetString("DatabaseName") ?? string.Empty;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        info.AddValue("Query", Query);
        info.AddValue("DatabaseName", DatabaseName);
    }
}
