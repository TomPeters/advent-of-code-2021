namespace AdventOfCode.Day3;

public static class Day3Puzzle
{
    // part 1
    public static int GetPowerConsumption(IEnumerable<string> diagnosticsReportString)
    {
        var binaryNumbers = diagnosticsReportString.Select(s => new BinaryNumber(s)).ToList();

        var gammaRate = GetPowerConsumptionFactor(binaryNumbers, new GammaRateBitCriteria());
        var epsilonRate = GetPowerConsumptionFactor(binaryNumbers, new EpsilonRateBitCriteria());

        return gammaRate.ConvertToInt() * epsilonRate.ConvertToInt();
    }

    static BinaryNumber GetPowerConsumptionFactor(IReadOnlyList<BinaryNumber> binaryNumbers, IBitCriteria bitCriteria)
    {
        var bits = Enumerable.Range(0, GetLengthOfEachBinaryNumber(binaryNumbers))
            .Select(index => GetBitAtIndex(index, binaryNumbers, bitCriteria));

        return new BinaryNumber(bits);
    }

    static bool GetBitAtIndex(int index, IReadOnlyList<BinaryNumber> binaryNumbers, IBitCriteria bitCriteria)
    {
        var bitsAtIndexAcrossAllNumbers = binaryNumbers.Select(n => n.GetBitAtIndex(index));
        var bitGroups = bitsAtIndexAcrossAllNumbers.GroupBy(n => n, (c, bits) => new BitGroups(c, bits.Count()));
        var bitSatisfyingBitCriteria = bitCriteria.GetBit(bitGroups);
        if (bitSatisfyingBitCriteria is { } result)
        {
            return result;
        }
        throw new Exception($"Binary numbers did not have any values at index {index}");
    }

    static int GetLengthOfEachBinaryNumber(IEnumerable<BinaryNumber> binaryNumbers)
    {
        var firstBinaryNumber = binaryNumbers.FirstOrDefault();
        return firstBinaryNumber?.GetLength() ?? 0;
    }
}

interface IBitCriteria
{
    bool? GetBit(IEnumerable<BitGroups> bitGroups);
}

class GammaRateBitCriteria : IBitCriteria
{
    public bool? GetBit(IEnumerable<BitGroups> bitGroups)
    {
        return bitGroups.MaxBy(group => group.Frequency)?.Bit;
    }
}

class EpsilonRateBitCriteria : IBitCriteria
{
    public bool? GetBit(IEnumerable<BitGroups> bitGroups)
    {
        return bitGroups.MinBy(group => group.Frequency)?.Bit;
    }
}

record BitGroups(bool Bit, int Frequency);

class BinaryNumber
{
    readonly bool[] _bits;

    public BinaryNumber(string number)
    {
        _bits = number.Select(c => c != '0').ToArray();
    }

    public BinaryNumber(IEnumerable<bool> number)
    {
        _bits = number.ToArray();
    }

    public bool GetBitAtIndex(int index)
    {
        return _bits[index];
    }

    public int GetLength()
    {
        return _bits.Length;
    }

    public int ConvertToInt()
    {
        return Convert.ToInt32(new string(_bits.Select(c => c ? '1' : '0').ToArray()), 2);
    }
}