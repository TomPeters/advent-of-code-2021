using System.Linq;
using AdventOfCode.Day14;
using Xunit;

namespace AdventOfCodeTests.Day14;

public class Day14
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(1, Day14Puzzle.DoThing(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day14Puzzle.DoThing(RealData));
    }

    static Day14Input SampleData => ParseInput(FileHelper.ReadFromFile("Day14", "SampleInstructions.txt"));
    static Day14Input RealData => ParseInput(FileHelper.ReadFromFile("Day14", "RealInstructions.txt"));

    static Day14Input ParseInput(string readFromFile)
    {
        var sections = readFromFile.Split("\n\n");
        var polymerTemplateString = sections[0];
        var polymerTemplate = new PolymerTemplate(polymerTemplateString);

        var pairInsertionRules = sections[1].Split("\n")
            .Select(pairInsertionRuleString =>
            {
                var pairInsertionRuleParts = pairInsertionRuleString.Split(" -> ");
                var output = pairInsertionRuleParts[1];
                var inputParts = pairInsertionRuleParts[0];
                return new PairInsertionRule(inputParts[0].ToString(), inputParts[1].ToString(), output);
            }).ToArray();
        
        return new Day14Input(polymerTemplate, pairInsertionRules);
    }
}