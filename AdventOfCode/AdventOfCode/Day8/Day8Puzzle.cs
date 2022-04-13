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
            var mapping = e.UniqueSignalPatterns.GetSignalWireMapping();
            return e.FourDigitOutputValue.ConvertToNumber(mapping);
        });
    }
}

public class SignalWireMapping
{
    readonly IDictionary<SignalWire, SignalWire> _mapping;
    static readonly IDictionary<ISet<SignalWire>, int> NumberToWireMappings = new Dictionary<ISet<SignalWire>, int>()
    {
        {new HashSet<SignalWire> { SignalWire.a, SignalWire.b, SignalWire.c, SignalWire.e, SignalWire.f, SignalWire.g}, 0},
        {new HashSet<SignalWire> { SignalWire.c, SignalWire.f }, 1},
        {new HashSet<SignalWire> { SignalWire.a, SignalWire.c, SignalWire.d, SignalWire.e, SignalWire.g}, 2},
        {new HashSet<SignalWire> { SignalWire.a, SignalWire.c, SignalWire.d, SignalWire.f, SignalWire.g}, 3},
        {new HashSet<SignalWire> { SignalWire.b, SignalWire.c, SignalWire.d, SignalWire.f }, 4},
        {new HashSet<SignalWire> { SignalWire.a, SignalWire.b, SignalWire.d, SignalWire.f, SignalWire.g}, 5},
        {new HashSet<SignalWire> { SignalWire.a, SignalWire.b, SignalWire.d, SignalWire.e, SignalWire.f, SignalWire.g}, 6},
        {new HashSet<SignalWire> { SignalWire.a, SignalWire.c, SignalWire.f }, 7},
        {new HashSet<SignalWire> { SignalWire.a, SignalWire.b, SignalWire.c, SignalWire.d, SignalWire.e, SignalWire.f, SignalWire.g}, 8},
        {new HashSet<SignalWire> { SignalWire.a, SignalWire.b, SignalWire.c, SignalWire.d, SignalWire.f, SignalWire.g}, 9}
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
        return NumberToWireMappings.Single(w => w.Key.SetEquals(intendedWires)).Value;
    }
}

public class Entry
{
    public Entry(UniqueSignalPatterns uniqueSignalPatterns, FourDigitOutputValue fourDigitOutputValue)
    {
        UniqueSignalPatterns = uniqueSignalPatterns;
        FourDigitOutputValue = fourDigitOutputValue;
    }

    public UniqueSignalPatterns UniqueSignalPatterns { get; }
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

public class UniqueSignalPatterns
{
    readonly IEnumerable<SignalPattern> _patterns;

    public UniqueSignalPatterns(IEnumerable<SignalPattern> patterns)
    {
        _patterns = patterns;
    }

    public SignalWireMapping GetSignalWireMapping()
    {
        var mapsToA = GetMappingForA();
        var mapsToE = GetMappingForE();
        var mapsToF = GetMappingForF(mapsToE);
        var mapsToC = GetMappingForC(mapsToF);
        var mapsToB = GetMappingForB(mapsToC, mapsToE);
        var mapsToD = GetMappingForD(mapsToB);
        var mapsToG = GetMappingForG(mapsToA, mapsToC, mapsToD, mapsToE);

        return new SignalWireMapping(mapsToA, mapsToB, mapsToC, mapsToD, mapsToE, mapsToF, mapsToG);
    }

    SignalWire GetMappingForA()
    {
        var patternFor1 = GetPatternWhere(p => p.Is1());
        var patternFor7 = GetPatternWhere(p => p.Is7());

        return patternFor7.DifferenceBetween(patternFor1).Single();
    }

    SignalWire GetMappingForE()
    {
        var patternFor4 = GetPatternWhere(p => p.Is4());
        var patternFor9 = GetPatternWhere(p => p.Is9(patternFor4));

        return patternFor9.MissingSignals().Single();
    }

    SignalWire GetMappingForF(SignalWire mapsToE)
    {
        var patternFor1 = GetPatternWhere(s => s.Is1());
        var patternFor2 = GetPatternWhere(s => s.Is2(mapsToE));
        var mapsToF = patternFor1.DifferenceBetween(patternFor2).Single();
        return mapsToF;
    }

    SignalWire GetMappingForC(SignalWire mapsToF)
    {
        return GetPatternWhere(s => s.Is1()).SignalsExcept(new [] { mapsToF }).Single();
    }

    SignalWire GetMappingForB(SignalWire mapsToC, SignalWire mapsToE)
    {
        var patternFor5 = GetPatternWhere(s => s.Is5(mapsToC));
        var patternFor3 = GetPatternWhere(s => s.Is3(mapsToC, mapsToE));
        var mapsToB = patternFor5.DifferenceBetween(patternFor3).Single();
        return mapsToB;
    }

    SignalWire GetMappingForD(SignalWire mapsToB)
    {
        var patternFor1 = GetPatternWhere(s => s.Is1());
        var patternFor4 = GetPatternWhere(s => s.Is4());
        var mapsToD = patternFor4.DifferenceBetween(patternFor1).Except(new[] { mapsToB }).Single();
        return mapsToD;
    }

    SignalWire GetMappingForG(SignalWire mapsToA, SignalWire mapsToC, SignalWire mapsToD, SignalWire mapsToE)
    {
        var patternFor2 = GetPatternWhere(p => p.Is2(mapsToE));
        return patternFor2.SignalsExcept(new[] { mapsToA, mapsToC, mapsToD, mapsToE }).Single();
    }

    SignalPattern GetPatternWhere(Predicate<SignalPattern> matches)
    {
        return _patterns.First(p => matches(p));
    }
}

public class SignalPattern
{
    readonly SignalWire[] _signalWires;

    public SignalPattern(IEnumerable<SignalWire> signalWires)
    {
        _signalWires = signalWires.ToArray();
    }

    public int ConvertToNumber(SignalWireMapping mapping)
    {
        return mapping.GetDigit(_signalWires);
    }

    public bool IsNumberWithUniqueNumberOfSegments()
    {
        return Is1() || Is4() || Is7() || Is8();
    }

    public bool Is1()
    {
        return _signalWires.Length == 2;
    }

    public bool Is2(SignalWire mapsToE)
    {
        return Is2Or3Or5() && _signalWires.Contains(mapsToE);
    }

    public bool Is3(SignalWire mapsToC, SignalWire mapsToE)
    {
        return Is2Or3Or5() && !Is5(mapsToC) && !Is2(mapsToE);
    }

    public bool Is4()
    {
        return _signalWires.Length == 4;
    }

    public bool Is5(SignalWire mapsToC)
    {
        return Is2Or3Or5() && !ContainsSignal(mapsToC);
    }

    bool Is2Or3Or5()
    {
        return _signalWires.Length == 5;
    }

    public bool Is7()
    {
        return _signalWires.Length == 3;
    }

    bool Is8()
    {
        return _signalWires.Length == 7;
    }

    public bool Is9(SignalPattern patternFor4)
    {
        return Is0Or6Or9() && patternFor4.IsContainedWithin(this);
    }

    bool Is0Or6Or9()
    {
        return _signalWires.Length == 6;
    }

    public SignalWire[] DifferenceBetween(SignalPattern otherPattern)
    {
        return _signalWires.Except(otherPattern._signalWires).ToArray();
    }

    bool IsContainedWithin(SignalPattern otherPattern)
    {
        return _signalWires.All(otherPattern._signalWires.Contains);
    }

    public SignalWire[] MissingSignals()
    {
        return Enum.GetValues<SignalWire>().Except(_signalWires).ToArray();
    }

    public SignalWire[] SignalsExcept(IEnumerable<SignalWire> signals)
    {
        return _signalWires.Except(signals).ToArray();
    }

    bool ContainsSignal(SignalWire signal)
    {
        return _signalWires.Contains(signal);
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