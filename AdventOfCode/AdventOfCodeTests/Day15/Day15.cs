using System.Linq;
using AdventOfCode.Day15;
using Xunit;

namespace AdventOfCodeTests.Day15;

public class Day15
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(40, Day15Puzzle.GetRiskOfLowestRiskPath(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day15Puzzle.GetRiskOfLowestRiskPath(RealData));
    }
    
    static Cave SampleData => ParseInput(FileHelper.ReadFromFile("Day15", "SampleCaveRiskLevels.txt"));
    static Cave RealData => ParseInput(FileHelper.ReadFromFile("Day15", "RealCaveRiskLevels.txt"));

    static Cave ParseInput(string input)
    {
        var riskLevelMatrix = input.Split("\n").Select(line =>
        {
            return line.Select(riskChar => int.Parse(riskChar.ToString())).ToArray();
        }).ToArray();
        return Cave.CreateCave(riskLevelMatrix);
    }
}