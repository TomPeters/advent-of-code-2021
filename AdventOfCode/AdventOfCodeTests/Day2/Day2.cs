using AdventOfCode.Day2;
using Xunit;

namespace AdventOfCodeTests.Day2;

public class Day2
{
    [Fact]
    public void WorksForSampleData()
    {
        Assert.Equal(150, Day2Puzzle.CalculateProductOfFinalPositionAndDepth(SampleData));
    }

    [Fact]
    public void Solve()
    {
        FileHelper.ReadFromFile("Day1", "Measurements.txt");
        Assert.Equal(1, Day2Puzzle.CalculateProductOfFinalPositionAndDepth(RealData));
    }

    static readonly string[] SampleData = {
        "forward 5",
        "down 5",
        "forward 8",
        "up 3",
        "down 8",
        "forward 2"
    };

    static string[] RealData => FileHelper.ReadFromFile("Day2", "Commands.txt").Split("\n");
}