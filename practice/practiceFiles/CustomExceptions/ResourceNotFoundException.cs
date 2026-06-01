namespace practice.practiceFiles.CustomExceptions;

public class ResourceNotFoundException<T> : Exception where T : class
{
    public Type ResourceType => typeof(T);
    public object ResourceKey { get; }

    public ResourceNotFoundException(object resourceKey)
      : base($"{typeof(T).Name} with key '{resourceKey}' was not found.")
    {
        ResourceKey = resourceKey;
    }

    public ResourceNotFoundException(object resourceKey, Exception innerException)
        : base($"{typeof(T).Name} with key '{resourceKey}' was not found.", innerException)
    {
        ResourceKey = resourceKey;
    }
}
