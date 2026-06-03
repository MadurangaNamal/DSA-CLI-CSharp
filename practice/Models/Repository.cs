using Practice.Exceptions;

namespace Practice.Models;

public class Repository<T> where T : class
{
    public T GetById(object id)
    {
        throw new ResourceNotFoundException<T>(id);   // Simulate not found
    }
}
