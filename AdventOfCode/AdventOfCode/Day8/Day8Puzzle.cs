namespace AdventOfCode.Day8;

public static class Day8Puzzle
{
    public static int SolvePuzzlePart1(IEnumerable<Entry> entries)
    {
        return entries.SelectMany(e => e.FourDigitOutputValue.FourSignalPatterns).Count(p => p.IsNumberWithUniqueNumberOfSegments());
    }
}

public class SignalPattern
{
    readonly string _signals;

    public SignalPattern(string signals)
    {
        _signals = signals;
    }

    public bool IsNumberWithUniqueNumberOfSegments()
    {
        var numberOfSegments = _signals.Length;
        return new[] { 2, 3, 4, 7 }.Contains(numberOfSegments);
    }
}

public class Entry
{
    public Entry(IEnumerable<SignalPattern> uniqueSignalPatterns, FourDigitOutputValue fourDigitOutputValue)
    {
        UniqueSignalPatterns = uniqueSignalPatterns.ToList();
        FourDigitOutputValue = fourDigitOutputValue;
    }

    public List<SignalPattern> UniqueSignalPatterns { get; }
    public FourDigitOutputValue FourDigitOutputValue { get; }
}

public class FourDigitOutputValue
{
    public FourDigitOutputValue(IEnumerable<SignalPattern> fourSignalPatterns)
    {
        FourSignalPatterns = fourSignalPatterns.ToList();
        if (FourSignalPatterns.Count() != 4)
        {
            throw new ArgumentException("Number of signal patterns must be exactly 4", nameof(fourSignalPatterns));
        }
    }

    public IEnumerable<SignalPattern> FourSignalPatterns { get; }
}