using AuroraUniversity.Domain;

public class Module
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Code { get; }
    public string Title { get; }
    public string ResponsibleStaff { get; }

    public int Capacity { get; }
    public List<Student> Students { get; } = new();
    public List<TeachingSession> Sessions { get; } = new();

    public Module(string code, string title, string responsibleStaff, int capacity)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Length < 3)
            throw new ArgumentException("Module code must be at least 3 characters.");
        if (capacity <= 0)
            throw new ArgumentException("Capacity must be positive.");

        Code = code;
        Title = title;
        ResponsibleStaff = responsibleStaff;
        Capacity = capacity;
    }

    public bool Enroll(Student student)
    {
        if (Students.Count >= Capacity) return false;
        if (!Students.Contains(student))
        {
            Students.Add(student);
            student.EnrolledModules.Add(this);
        }
        return true;
    }
}