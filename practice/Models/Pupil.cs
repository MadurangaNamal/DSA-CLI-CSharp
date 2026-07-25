namespace Practice.Models;

public class Pupil
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public Pupil() { }

    public Pupil(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string? ToString()
    {
        return $"Pupil: {Name} (Id: {Id})";
    }
}
