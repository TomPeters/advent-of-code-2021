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

    static int GetLengthOfEachBinaryNumber(IEnumerable<BinaryNumber> binaryNumbers)
    {
        var firstBinaryNumber = binaryNumbers.FirstOrDefault();
        return firstBinaryNumber?.GetLength() ?? 0;
    }

    static BinaryNumber GetPowerConsumptionFactor(IReadOnlyList<BinaryNumber> binaryNumbers, IBitCriteria bitCriteria)
    {
        var factorChars = Enumerable.Range(0, GetLengthOfEachBinaryNumber(binaryNumbers)).Select(index =>
        {
            var allDigitsAtIndex = binaryNumbers.Select(n => n.GetValueAtIndex(index));
            var digitGroups = allDigitsAtIndex.GroupBy(n => n, (c, chars) => new DigitGroups(c, chars.Count()));
            var digitSatisfyingBitCriteria = bitCriteria.GetDigit(digitGroups);
            if (digitSatisfyingBitCriteria is { } bitCriteriaDigit)
            {
                return bitCriteriaDigit;
            }
            throw new Exception($"Binary numbers did not have any values at index {index}");
        }).ToArray();

        return new BinaryNumber(new string(factorChars));
    }
}

interface IBitCriteria
{
    char? GetDigit(IEnumerable<DigitGroups> digitGroups);
}

class GammaRateBitCriteria : IBitCriteria
{
    public char? GetDigit(IEnumerable<DigitGroups> digitGroups)
    {
        return digitGroups.MaxBy(group => group.Frequency)?.Digit;
    }
}

class EpsilonRateBitCriteria : IBitCriteria
{
    public char? GetDigit(IEnumerable<DigitGroups> digitGroups)
    {
        return digitGroups.MinBy(group => group.Frequency)?.Digit;
    }
}

record DigitGroups(char Digit, int Frequency);

class BinaryNumber
{
    readonly char[] _chars;

    public BinaryNumber(string number)
    {
        _chars = number.Select(c => c).ToArray();
    }

    public char GetValueAtIndex(int index)
    {
        return _chars[index];
    }

    public int GetLength()
    {
        return _chars.Count();
    }

    public int ConvertToInt()
    {
        return Convert.ToInt32(new string(_chars), 2);
    }
}