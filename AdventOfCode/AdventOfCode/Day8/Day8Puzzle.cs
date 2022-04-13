using AdventOfCode.Utils;

namespace AdventOfCode.Day8;

public static class Day8Puzzle
{
    public static int NumberOfOutputDigitsThatAreUniquelyIdentifiableBySignalCount(IEnumerable<Entry> entries)
    {
        return entries.SelectMany(e => e.FourDigitOutputValue.FourSignalPatterns).Count(p => p.IsNumberWithUniqueNumberOfSegments());
    }

    public static int SumOfOutputDigits(IEnumerable<Entry> entries)
    {
        return entries.Sum(e =>
        {
            var mapping = e.GetSignalWireMapping();
            return e.FourDigitOutputValue.ConvertToNumber(mapping);
        });
    }
}

public class SignalPattern
{
    readonly string _signals;

    public SignalPattern(string signals)
    {
        _signals = signals;
    }

    public int ConvertToNumber(SignalWireMapping mapping)
    {
        // TODO
        return 0;
    }

    public bool IsNumberWithUniqueNumberOfSegments()
    {
        var numberOfSegments = _signals.Length;
        return new[] { 2, 3, 4, 7 }.Contains(numberOfSegments);
    }
}

public class SignalWireMapping {}

public class Entry
{
    public Entry(IEnumerable<SignalPattern> uniqueSignalPatterns, FourDigitOutputValue fourDigitOutputValue)
    {
        UniqueSignalPatterns = uniqueSignalPatterns.ToList();
        FourDigitOutputValue = fourDigitOutputValue;
    }

    public SignalWireMapping GetSignalWireMapping()
    {
        // 1 - 2
        // 7 - 3

        // 4 - 4

        // 2 - 5
        // 3 - 5
        // 5 - 5

        // 0 - 6
        // 6 - 6
        // 9 - 6

        // disregard 8, it gives us no information
        // 8 - 7

        // Find 1 and 7. This gives us the "a" position
        // Find the "6" digited number that completely overlaps with 4 - this gives us 9. The missing signal from 9 gives us the "e" position
        // Finding the "5" digited number that contains "e" - this gives us 2.
        // from 2, work out which signal from 1 is missing from 2 - this gives us "f"
        // The signal from 1 that isn't "f" gives us "c"
        // we now have "a", "e", "f", "c"
        // Find the 5 digited number that doesn't contain "c" - this is 5, and the remaining 5 digited number is 3.
        // The signal in 5 that isn't in 3 is "b".
        // The signal in 4 that isn't "b" and isn't in "1" is "d"
        // The remaining signal is "g".

        // alternative
        // the signal from the 5 digited numbers that has 4 overlapping digits with the other 5 digited numbers is 3. The difference between this signal and 4 is "b".
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

    public int ConvertToNumber(SignalWireMapping mapping)
    {
        return FourSignalPatterns.Reverse().Select((s, magnitude) => s.ConvertToNumber(mapping) * 10.Pow(magnitude)).Sum();
    }

    public IEnumerable<SignalPattern> FourSignalPatterns { get; }
}