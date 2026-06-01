using practice.practiceFiles.CustomExceptions;

namespace practice.practiceFiles.Models;

public class UserRegistration
{
    public void RegisterUser(string name, int age)
    {
        if (age < 18)
            throw new InvalidAgeException($"Age {age} is invalid. User must be at least 18 years old.");

        Console.WriteLine($"User {name} registered successfully.");
    }
}
