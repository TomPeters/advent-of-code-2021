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
    readonly SignalWire[] _signalWires;

    public SignalPattern(string signalWires)
    {
        _signalWires = signalWires.Select(c => Enum.Parse<SignalWire>(c.ToString())).ToArray();
    }

    public int ConvertToNumber(SignalWireMapping mapping)
    {
        return mapping.GetDigit(_signalWires);
    }

    public bool IsNumberWithUniqueNumberOfSegments()
    {
        var numberOfSegments = _signalWires.Length;
        return new[] { 2, 3, 4, 7 }.Contains(numberOfSegments);
    }
}

public enum SignalWire
{
    a,
    b,
    c,
    d,
    e,
    f,
    g
}

public class SignalWireMapping
{
    readonly IDictionary<SignalWire, SignalWire> _mapping;
    static readonly IDictionary<IEnumerable<SignalWire>, int> NumberToWireMappings = new Dictionary<IEnumerable<SignalWire>, int>()
    {
        {new [] { SignalWire.a, SignalWire.b, SignalWire.c, SignalWire.e, SignalWire.f, SignalWire.g}, 0},
        {new [] { SignalWire.c, SignalWire.f }, 1},
        {new [] { SignalWire.a, SignalWire.c, SignalWire.d, SignalWire.e, SignalWire.g}, 2},
        {new [] { SignalWire.a, SignalWire.c, SignalWire.d, SignalWire.f, SignalWire.g}, 3},
        {new [] { SignalWire.b, SignalWire.c, SignalWire.d, SignalWire.f }, 4},
        {new [] { SignalWire.a, SignalWire.b, SignalWire.d, SignalWire.f, SignalWire.g}, 5},
        {new [] { SignalWire.a, SignalWire.b, SignalWire.d, SignalWire.e, SignalWire.f, SignalWire.g}, 6},
        {new [] { SignalWire.a, SignalWire.c, SignalWire.f }, 7},
        {new [] { SignalWire.a, SignalWire.b, SignalWire.c, SignalWire.d, SignalWire.e, SignalWire.f, SignalWire.g}, 8},
        {new [] { SignalWire.a, SignalWire.b, SignalWire.c, SignalWire.d, SignalWire.f, SignalWire.g}, 9}
    };

    public SignalWireMapping(SignalWire mapsToA, SignalWire mapsToB, SignalWire mapsToC, SignalWire mapsToD, SignalWire mapsToE, SignalWire mapsToF, SignalWire mapsToG)
    {
        _mapping = new Dictionary<SignalWire, SignalWire>()
        {
            { mapsToA, SignalWire.a },
            { mapsToB, SignalWire.b },
            { mapsToC, SignalWire.c },
            { mapsToD, SignalWire.d },
            { mapsToE, SignalWire.e },
            { mapsToF, SignalWire.f },
            { mapsToG, SignalWire.g }
        };
    }

    public int GetDigit(IEnumerable<SignalWire> mixedUpWires)
    {
        var intendedWires = mixedUpWires.Select(w => _mapping[w]).ToArray();
        return NumberToWireMappings.Single(w => !w.Key.Except(intendedWires).Any()).Value;
    }
}

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