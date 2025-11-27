public class TeachingSession
{
    public Guid Id { get; } = Guid.NewGuid();
    public Module Module { get; }
    public DateTime UtcDateTime { get; }
    public string LocationOrLink { get; }

    public TeachingSession(Module module, DateTime utcDateTime, string locationOrLink)
    {
        Module = module ?? throw new ArgumentNullException(nameof(module));
        UtcDateTime = utcDateTime;

        if (string.IsNullOrWhiteSpace(locationOrLink))
            throw new ArgumentException("Location or link is required.");
        LocationOrLink = locationOrLink;
    }

    public bool ConflictsWith(TeachingSession other, TimeSpan tolerance)
    {
        return Math.Abs((UtcDateTime - other.UtcDateTime).TotalMinutes) < tolerance.TotalMinutes;
    }
}