public class Mark
{
    public Guid Id { get; } = Guid.NewGuid();
    public Student Student { get; }
    public Module Module { get; }

    public int FirstAttempt { get; }
    public int? ResitAttempt { get; private set; }

    public int EffectiveScore => ResitAttempt.HasValue ? Math.Max(FirstAttempt, ResitAttempt.Value) : FirstAttempt;

    public Mark(Student student, Module module, int firstAttempt)
    {
        Student = student ?? throw new ArgumentNullException(nameof(student));
        Module = module ?? throw new ArgumentNullException(nameof(module));
        ValidateScore(firstAttempt);
        FirstAttempt = firstAttempt;
    }

    public void RecordResit(int score)
    {
        ValidateScore(score);
        ResitAttempt = score;
    }

    private static void ValidateScore(int score)
    {
        if (score < 0 || score > 100)
            throw new ArgumentOutOfRangeException(nameof(score), "Score must be between 0 and 100.");
    }
}