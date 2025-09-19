namespace practice.practiceFiles;

public class PersonDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateOnly BirthDate { get; set; }
    public string Phone { get; set; }

    public PersonDto()
    {
    }

    public PersonDto(string firstName, string lastName, string emailAddress, DateOnly birthDate, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = emailAddress;
        BirthDate = birthDate;
        Phone = phone;
    }
}
