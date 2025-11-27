using AuroraUniversity.Domain;

public class Student
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }

    public List<Module> EnrolledModules { get; } = new();
    public List<Mark> Marks { get; } = new();

    public Student(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Student name is required.");
        Name = name;
    }
}