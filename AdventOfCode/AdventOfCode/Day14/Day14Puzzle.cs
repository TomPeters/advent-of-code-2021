namespace AdventOfCode.Day14;

public static class Day14Puzzle
{
    public static int DoThing(Day14Input input)
    {
        return 0;
    }
}

public record Day14Input(PolymerTemplate PolymerTemplate, PairInsertionRule[] PairInsertionRules);

public class PolymerTemplate
{
    readonly string _input;

    public PolymerTemplate(string input)
    {
        _input = input;
    }
}

public class PairInsertionRule
{
    readonly string _firstInput;
    readonly string _secondInput;
    readonly string _output;

    public PairInsertionRule(string firstInput, string secondInput, string output)
    {
        _firstInput = firstInput;
        _secondInput = secondInput;
        _output = output;
    }
}
