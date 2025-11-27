public class AssessedItem
{
    public string Name { get; }
    public double Weight { get; } 
    public DateTime AssessmentDateUtc { get; }
    public Mark Mark { get; }

    public AssessedItem(string name, double weight, DateTime dateUtc, Mark mark)
    {
        if (weight < 0 || weight > 1)
            throw new ArgumentException("Weight must be between 0 and 1.");
        Name = name;
        Weight = weight;
        AssessmentDateUtc = dateUtc;
        Mark = mark ?? throw new ArgumentNullException(nameof(mark));
    }

    public int EffectiveScore => Mark.EffectiveScore;
}
