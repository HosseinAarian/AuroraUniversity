using System.Text.RegularExpressions;

public class UniversityCode
{
    public string Type { get; }
    public int Year { get; }
    public int Term { get; }
    public string Code { get; }
    public int Serial { get; }
    public int Checksum { get; }

    private UniversityCode(string type, int year, int term, string code, int serial, int checksum)
    {
        Type = type;
        Year = year;
        Term = term;
        Code = code;
        Serial = serial;
        Checksum = checksum;
    }

    public static UniversityCode Parse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input is empty.");

        string trimmed = input.Trim().ToUpper();

        var regex = new Regex(@"^AU-(STU|MOD|STF)-(\d{4})([1-3])-([A-Z]{3,6})-(\d{4})-(\d{2})$");
        var match = regex.Match(trimmed);

        if (!match.Success)
            throw new FormatException("Code format is invalid. Expected AU-TYPE-YEARTERM-CODE-NNNN-CC");

        string type = match.Groups[1].Value;
        if (!new[] { "STU", "MOD", "STF" }.Contains(type))
            throw new FormatException($"Invalid TYPE: {type}. Must be STU, MOD, or STF.");

        int year = int.Parse(match.Groups[2].Value);
        if (year < 1900 || year > 2100)
            throw new FormatException($"Invalid YEAR: {year}. Must be 4 digits between 1900 and 2100.");

        int term = int.Parse(match.Groups[3].Value);
        if (term < 1 || term > 3)
            throw new FormatException($"Invalid TERM: {term}. Must be 1, 2, or 3.");

        string code = match.Groups[4].Value;
        if (code.Length < 3 || code.Length > 6)
            throw new FormatException($"Invalid CODE length: {code}. Must be 3-6 letters.");

        int serial = int.Parse(match.Groups[5].Value);
        int checksum = int.Parse(match.Groups[6].Value);

        string concatenated = (trimmed.Replace("-", ""));
        int sum = 0;
        foreach (char c in concatenated.Substring(2, concatenated.Length - 4)) 
        {
            if (char.IsDigit(c)) sum += c - '0';
            else if (char.IsLetter(c)) sum += c - 'A' + 10;
            else throw new FormatException($"Invalid character in code: {c}");
        }
        int calcChecksum = sum % 97;

        if (calcChecksum != checksum)
            throw new FormatException($"Checksum mismatch: expected {checksum}, calculated {calcChecksum}.");

        return new UniversityCode(type, year, term, code, serial, checksum);
    }

    public override string ToString()
    {
        return $"AU-{Type}-{Year}{Term}-{Code}-{Serial:D4}-{Checksum:D2}";
    }
}
