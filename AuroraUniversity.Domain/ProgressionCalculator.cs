public static class ProgressionCalculator
{
    public static (double FinalAverage, DateTime MinDate, DateTime MaxDate, string Decision)
        CalculateTermResult(Student student, List<Module> modules, Dictionary<Module, List<AssessedItem>> moduleAssessments, Dictionary<Module, double> moduleCredits)
    {
        var moduleMarks = new List<(double WeightedMark, double Credit)>();
        DateTime? minDate = null;
        DateTime? maxDate = null;

        foreach (var module in modules)
        {
            if (!moduleAssessments.TryGetValue(module, out var items) || items.Count == 0)
                continue;

            double moduleMark = items.Sum(i => i.EffectiveScore * i.Weight);
            moduleMarks.Add((moduleMark, moduleCredits.ContainsKey(module) ? moduleCredits[module] : 1.0));

            var dates = items.Select(i => i.AssessmentDateUtc);
            minDate = minDate.HasValue ? (dates.Min() < minDate.Value ? dates.Min() : minDate.Value) : dates.Min();
            maxDate = maxDate.HasValue ? (dates.Max() > maxDate.Value ? dates.Max() : maxDate.Value) : dates.Max();
        }

        double finalAverage = 0.0;
        double totalCredits = moduleMarks.Sum(m => m.Credit);
        if (totalCredits > 0)
        {
            finalAverage = moduleMarks.Sum(m => m.WeightedMark * m.Credit) / totalCredits;
            finalAverage = Math.Round(finalAverage, 1, MidpointRounding.AwayFromZero);
        }

        string decision = finalAverage >= 40.0 ? "Pass" : "Refer";

        return (finalAverage, minDate ?? DateTime.MinValue, maxDate ?? DateTime.MinValue, decision);
    }
}
