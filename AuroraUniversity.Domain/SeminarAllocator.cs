public static class SeminarAllocator
{
    public static (Dictionary<SeminarGroup, List<Student>> assignment, List<Student> unplaced)
        AllocateStudents(Module module, List<SeminarGroup> groups, Dictionary<Student, List<TeachingSession>> studentTimetables)
    {
        var assignment = groups.ToDictionary(g => g, g => new List<Student>());
        var unplaced = new List<Student>();

        var students = module.Students.OrderBy(s => s.Name).ToList();

        foreach (var student in students)
        {
            bool placed = false;

            foreach (var group in groups)
            {
                if (!group.HasSpace) continue;

                var conflicts = studentTimetables.TryGetValue(student, out var timetable)
                    ? timetable.Any(s => s.UtcDateTime.DayOfWeek == group.Day &&
                                         TimeOverlap(s.UtcDateTime.TimeOfDay, s.UtcDateTime.TimeOfDay + TimeSpan.FromHours(1), group.StartTime, group.EndTime))
                    : false;

                if (!conflicts)
                {
                    group.AssignedStudents.Add(student);
                    assignment[group].Add(student);
                    placed = true;
                    break;
                }
            }

            if (!placed)
                unplaced.Add(student);
        }

        return (assignment, unplaced);
    }

    private static bool TimeOverlap(TimeSpan start1, TimeSpan end1, TimeSpan start2, TimeSpan end2)
    {
        return start1 < end2 && start2 < end1;
    }
}
