using AdventOfCode.Day10;
using Xunit;

namespace AdventOfCodeTests.Day10;

public class Day10
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(1, Day10Puzzle.GetTotalSyntaxErrorScore(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day10Puzzle.GetTotalSyntaxErrorScore(RealData));
    }

    static string RealData => FileHelper.ReadFromFile("Day10", "RealNavigationSubsystem.txt");

    static string SampleData => FileHelper.ReadFromFile("Day10", "SampleNavigationSubsystem.txt");
}
