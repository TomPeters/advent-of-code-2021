namespace AdventOfCode.Day14;

public static class Day14Puzzle
{
    public static long DoThing(Day14Input input, int numberOfSteps)
    {
        var finalPolymerTemplate = Enumerable.Range(0, numberOfSteps)
            .Aggregate(input.PolymerTemplate, (prev, stepNumber) =>
            {
                var polymerTemplate = prev.Step(input.PairInsertionRules);
                Console.WriteLine(stepNumber);
                return polymerTemplate;
            });
        return finalPolymerTemplate.QuantityOfMostCommonElement() - finalPolymerTemplate.QuantityOfLeastCommonElement();
    }
}

public record Day14Input(PolymerTemplate PolymerTemplate, PairInsertionRule[] PairInsertionRules);

public class PolymerTemplate
{
    readonly List<Pair> _pairs;

    public PolymerTemplate(string input)
    {
        _pairs = input.Zip(input.Skip(1), (first, second) => new Pair(first, second)).ToList();
    }

    PolymerTemplate(List<Pair> pairs)
    {
        _pairs = pairs;
    }

    public PolymerTemplate Step(PairInsertionRule[] rules)
    {
        var newPairs = _pairs.SelectMany(p => GetNewPairs(p, rules)).ToList();
        return new PolymerTemplate(newPairs);
    }

    IEnumerable<Pair> GetNewPairs(Pair pair, PairInsertionRule[] rules)
    {
        var matchingRule = rules.First(p => p.Matches(pair));
        yield return pair with { SecondElement = matchingRule.OutputElement };
        yield return pair with { FirstElement = matchingRule.OutputElement };
    }

    public long QuantityOfMostCommonElement()
    {
        return Elements()
            .GroupBy(e => e)
            .Max(e => e.Count());
    }

    public long QuantityOfLeastCommonElement()
    {
        return Elements()
            .GroupBy(e => e)
            .Min(e => e.Count());
    }

    public string Debug()
    {
        return new string(Elements().ToArray());
    }

    IEnumerable<char> Elements()
    {
        var first = _pairs.First();
        yield return first.FirstElement;
        foreach (var pair in _pairs)
        {
            yield return pair.SecondElement;
        }
    } 
}

public class PairInsertionRule
{
    public char OutputElement { get; }
    readonly Pair _pair;

    public PairInsertionRule(Pair pair, char outputElement)
    {
        OutputElement = outputElement;
        _pair = pair;
    }

    public bool Matches(Pair pair)
    {
        return _pair.Equals(pair);
    }
}

public record Pair(char FirstElement, char SecondElement);
