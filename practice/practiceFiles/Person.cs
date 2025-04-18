
namespace practice.practiceFiles;

public class Person
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string PhoneNumber { get; set; }

    public Person()
    {
    }

    public static void ToString(Person person)
    {
        Console.WriteLine($"Person: FullName: {person.FullName}, Email: {person.Email}, Age: {person.Age}, PhoneNumber: {person.PhoneNumber}");
    }
}
