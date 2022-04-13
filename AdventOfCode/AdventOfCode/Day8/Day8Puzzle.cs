namespace AdventOfCode.Day8;

public static class Day8Puzzle
{
    public static int SolvePuzzle(IEnumerable<Entry> entries)
    {
        return 0;
    }
}

public class SignalPattern
{
    public SignalPattern(string signals)
    {
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