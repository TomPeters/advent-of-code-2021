namespace AdventOfCode.Day14;

public static class Day14Puzzle
{
    public static long GetDifferenceBetweenMostCommonElementCountAndLeastCommonElementCountAfterNumberOfSteps(Day14Input input, int numberOfSteps)
    {
        var countsAfterNumberOfSteps = input.PolymerTemplate.GetElementCountsAfterNumberOfSteps(numberOfSteps, input.PairInsertionRules);
        return countsAfterNumberOfSteps.QuantityOfMostCommonElement() - countsAfterNumberOfSteps.QuantityOfLeastCommonElement();
    }
}

public record Day14Input(PolymerTemplate PolymerTemplate, PairInsertionRule[] PairInsertionRules);

public class ElementCounts
{
    readonly IDictionary<char, long> _elementCounts;
    readonly char _lastElementInPolymerTemplate;

    ElementCounts(IDictionary<char, long> elementCounts, char lastElementInPolymerTemplate)
    {
        _elementCounts = elementCounts;
        _lastElementInPolymerTemplate = lastElementInPolymerTemplate;
    }

    public static ElementCounts CreateForPair(Pair pair)
    {
        if (pair.FirstElement == pair.SecondElement)
            return new ElementCounts(new Dictionary<char, long>() { { pair.FirstElement, 2 } }, pair.SecondElement);

        return new ElementCounts(new Dictionary<char, long>()
        {
            { pair.FirstElement, 1 },
            { pair.SecondElement, 1 },
        }, pair.SecondElement);
    } 

    public static ElementCounts CombineCountsOfOverlappingPolymerTemplates(IEnumerable<ElementCounts> elementCounts)
    {
        return elementCounts.Aggregate((prev, cur) => prev.CombineCountsOfOverlappingPolymerTemplates(cur));
    }

    public ElementCounts CombineCountsOfOverlappingPolymerTemplates(ElementCounts countsOfSecondPolymerTemplate)
    {
        var firstElementCounts = _elementCounts;
        var secondElementCounts = countsOfSecondPolymerTemplate._elementCounts;
        var allPresentElements = firstElementCounts.Keys.Concat(secondElementCounts.Keys);
        var newCounts = new Dictionary<char, long>();
        foreach (var element in allPresentElements)
        {
            long firstCounts = firstElementCounts.TryGetValue(element, out var firstCount) ? firstCount : 0;
            long secondCounts = secondElementCounts.TryGetValue(element, out var secondCount) ? secondCount : 0;
            newCounts[element] = firstCounts + secondCounts;
        }

        // this element has been double counted
        newCounts[_lastElementInPolymerTemplate] -= 1;

        return new ElementCounts(newCounts, countsOfSecondPolymerTemplate._lastElementInPolymerTemplate);
    }
    
    public long QuantityOfMostCommonElement() => _elementCounts.Values.MaxBy(v => v);

    public long QuantityOfLeastCommonElement() => _elementCounts.Values.MinBy(v => v);
}

public class ElementCountCache
{
    readonly IDictionary<CacheKey, ElementCounts> _elementCountsCache = new Dictionary<CacheKey, ElementCounts>();
    public ElementCounts? TryGet(Pair pair, int numberOfSteps)
    {
        var key = new CacheKey(pair, numberOfSteps);
        return _elementCountsCache.TryGetValue(key, out var elementCounts) ? elementCounts : null;
    }
    
    public void Add(Pair pair, int numberOfSteps, ElementCounts elementCounts)
    {
        var key = new CacheKey(pair, numberOfSteps);
        _elementCountsCache[key] = elementCounts;
    }
    
    record CacheKey(Pair Pair, int NumberOfSteps);
}

public class PolymerTemplate
{
    readonly List<Pair> _pairs;

    public PolymerTemplate(string input)
    {
        _pairs = input.Zip(input.Skip(1), (first, second) => new Pair(first, second)).ToList();
    }
    
    public ElementCounts GetElementCountsAfterNumberOfSteps(int numberOfSteps, PairInsertionRule[] rules)
    {
        var cache = new ElementCountCache();
        return ElementCounts.CombineCountsOfOverlappingPolymerTemplates(_pairs.Select(pair => GetElementCountAfterNumberOfSteps(pair, numberOfSteps, rules, cache)));
    }

    ElementCounts GetElementCountAfterNumberOfSteps(Pair pair, int numberOfSteps, PairInsertionRule[] rules, ElementCountCache cache)
    {
        var cachedValue = cache.TryGet(pair, numberOfSteps);
        if (cachedValue is not null) return cachedValue;
        if (numberOfSteps == 0)
        {
            return ElementCounts.CreateForPair(pair);
        }
        
        var matchingRule = rules.First(p => p.Matches(pair));
        var firstChildPair = pair with { SecondElement = matchingRule.OutputElement };
        var secondChildPair = pair with { FirstElement = matchingRule.OutputElement };
        var countsOfFirstChildPair = GetElementCountAfterNumberOfSteps(firstChildPair, numberOfSteps - 1, rules, cache);
        var countsOfSecondChildPair = GetElementCountAfterNumberOfSteps(secondChildPair, numberOfSteps - 1, rules, cache);
        var elementCounts = countsOfFirstChildPair.CombineCountsOfOverlappingPolymerTemplates(countsOfSecondChildPair);
        cache.Add(pair, numberOfSteps, elementCounts);
        return elementCounts;
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
