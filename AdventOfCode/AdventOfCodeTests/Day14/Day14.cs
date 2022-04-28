using System;
using System.Linq;
using AdventOfCode.Day14;
using Xunit;

namespace AdventOfCodeTests.Day14;

public class Day14
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(1588, Day14Puzzle.GetDifferenceBetweenMostCommonElementCountAndLeastCommonElementCountAfterNumberOfSteps(SampleData, 10));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(5656, Day14Puzzle.GetDifferenceBetweenMostCommonElementCountAndLeastCommonElementCountAfterNumberOfSteps(RealData, 10));
    }
    
    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(2188189693529L, Day14Puzzle.GetDifferenceBetweenMostCommonElementCountAndLeastCommonElementCountAfterNumberOfSteps(SampleData, 40));
    }

    [Fact]
    public void Part2WorksForRealData()
    {
        Assert.Equal(12271437788530L, Day14Puzzle.GetDifferenceBetweenMostCommonElementCountAndLeastCommonElementCountAfterNumberOfSteps(RealData, 40));
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
                var output = Convert.ToChar(pairInsertionRuleParts[1]);
                var inputParts = pairInsertionRuleParts[0];
                return new PairInsertionRule(new Pair(inputParts[0], inputParts[1]), output);
            }).ToArray();
        
        return new Day14Input(polymerTemplate, pairInsertionRules);
    }
}