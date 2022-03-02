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

    // part 2
    public static int GetLifeSupportRating(IEnumerable<string> diagnosticsReportString)
    {
        var binaryNumbers = diagnosticsReportString.Select(s => new BinaryNumber(s)).ToList();

        var oxygenGeneratorRating = GetLifeSupportRatingFactor(binaryNumbers, new OxygenGeneratorRating());
        var CO2ScrubberRating = GetLifeSupportRatingFactor(binaryNumbers, new CO2ScrubberRating());

        return oxygenGeneratorRating.ConvertToInt() * CO2ScrubberRating.ConvertToInt();
    }

    static BinaryNumber GetLifeSupportRatingFactor(IReadOnlyList<BinaryNumber> binaryNumbers, IBitCriteria bitCriteria)
    {
        var filteredBinaryNumbers = Enumerable.Range(0, GetLengthOfEachBinaryNumber(binaryNumbers))
            .Aggregate(binaryNumbers, (accumulatedBinaryNumbers, index) =>
            {
                if (accumulatedBinaryNumbers.Count == 1) return accumulatedBinaryNumbers;

                var bit = GetBitAtIndex(index, accumulatedBinaryNumbers, bitCriteria);
                var binaryNumbersWithBitAtPosition = accumulatedBinaryNumbers.Where(n => n.GetBitAtIndex(index) != bit);
                return accumulatedBinaryNumbers.Except(binaryNumbersWithBitAtPosition).ToList();
            });

        return filteredBinaryNumbers.Single();
    }

    static bool GetBitAtIndex(int index, IEnumerable<BinaryNumber> binaryNumbers, IBitCriteria bitCriteria)
    {
        var bitsAtIndexAcrossAllNumbers = binaryNumbers.Select(n => n.GetBitAtIndex(index)).ToArray();
        var countOfTrueBits = bitsAtIndexAcrossAllNumbers.Count(b => b);
        var countOfFalseBits = bitsAtIndexAcrossAllNumbers.Count(b => !b);
        return bitCriteria.GetBit(countOfTrueBits, countOfFalseBits);
    }

    static int GetLengthOfEachBinaryNumber(IEnumerable<BinaryNumber> binaryNumbers)
    {
        var firstBinaryNumber = binaryNumbers.FirstOrDefault();
        return firstBinaryNumber?.GetLength() ?? 0;
    }
}

interface IBitCriteria
{
    bool GetBit(int countOfTrueBits, int countOfFalseBits);
}

class GammaRateBitCriteria : IBitCriteria
{
    public bool GetBit(int countOfTrueBits, int countOfFalseBits)
    {
        return countOfTrueBits < countOfFalseBits;
    }
}

class EpsilonRateBitCriteria : IBitCriteria
{
    public bool GetBit(int countOfTrueBits, int countOfFalseBits)
    {
        return countOfTrueBits > countOfFalseBits;
    }
}

class OxygenGeneratorRating : IBitCriteria
{
    public bool GetBit(int countOfTrueBits, int countOfFalseBits)
    {
        if (countOfTrueBits == countOfFalseBits) return true;
        return countOfFalseBits <= countOfTrueBits;
    }
}

class CO2ScrubberRating : IBitCriteria
{
    public bool GetBit(int countOfTrueBits, int countOfFalseBits)
    {
        if (countOfTrueBits == countOfFalseBits) return false;
        return countOfFalseBits >= countOfTrueBits;
    }
}

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