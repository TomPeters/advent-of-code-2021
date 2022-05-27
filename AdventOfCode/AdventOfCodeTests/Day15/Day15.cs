using AdventOfCode.Day15;
using Xunit;

namespace AdventOfCodeTests.Day15;

public class Day15
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(1, Day15Puzzle.GetRiskOfLowestRiskPath(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day15Puzzle.GetRiskOfLowestRiskPath(RealData));
    }
    
    static string SampleData => FileHelper.ReadFromFile("Day15", "SampleCaveRiskLevels.txt");
    static string RealData => FileHelper.ReadFromFile("Day15", "RealCaveRiskLevels.txt");
}