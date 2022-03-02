namespace AdventOfCode.Day3;

public static class Day3Puzzle
{
    // part 1
    public static int GetSubmarinePowerConsumption(IEnumerable<string> diagnosticsReportString)
    {
        var binaryStringsForGammaRate = diagnosticsReportString
            .Select(s => new BinaryNumber(s))
            .SelectMany(b => b.GetDigitsAndPositions())
            .GroupBy(dap => dap.Position)
            .OrderBy(digitGroup => digitGroup.Key)
            .Select(digitGroup =>
            {
                return digitGroup.Select(g => g.Digit).GroupBy(digit => digit).MaxBy(g => g.Count())!.Key;
            });
        var gammaRate = new BinaryNumber(new string(binaryStringsForGammaRate.ToArray()));

        var binaryStringsForEpsilonRate = diagnosticsReportString
            .Select(s => new BinaryNumber(s))
            .SelectMany(b => b.GetDigitsAndPositions())
            .GroupBy(dap => dap.Position)
            .OrderBy(digitGroup => digitGroup.Key)
            .Select(digitGroup =>
            {
                return digitGroup.Select(g => g.Digit).GroupBy(digit => digit).MinBy(g => g.Count())!.Key;
            });
        var epsilonRate = new BinaryNumber(new string(binaryStringsForEpsilonRate.ToArray()));
        return gammaRate.ConvertToInt() * epsilonRate.ConvertToInt();
    }
}

class BinaryNumber
{
    readonly string _number;

    public BinaryNumber(string number)
    {
        _number = number;
    }

    public IReadOnlyList<DigitAndPosition> GetDigitsAndPositions()
    {
        return _number.Select((d, i) => new DigitAndPosition(d, i)).ToList();
    }

    public int ConvertToInt()
    {
        return Convert.ToInt32(_number, 2);
    }
}

record DigitAndPosition(char Digit, int Position);
