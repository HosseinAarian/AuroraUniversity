

public class SeminarGroup
{
    public string Name { get; }
    public int Capacity { get; }
    public DayOfWeek Day { get; }
    public TimeSpan StartTime { get; }
    public TimeSpan EndTime { get; }

    public List<Student> AssignedStudents { get; } = new();

    public SeminarGroup(string name, int capacity, DayOfWeek day, TimeSpan start, TimeSpan end)
    {
        Name = name;
        Capacity = capacity;
        Day = day;
        StartTime = start;
        EndTime = end;
    }

    public bool HasSpace => AssignedStudents.Count < Capacity;

    public bool ConflictsWith(TimeSpan otherStart, TimeSpan otherEnd)
    {
        return !(EndTime <= otherStart || StartTime >= otherEnd);
    }
}
