using AdventOfCode.Day2;
using Xunit;

namespace AdventOfCodeTests.Day2;

public class Day2
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(150, Day2Puzzle.Part1CalculateProductOfFinalPositionAndDepth(SampleData));
    }

    [Fact]
    public void SolvePart1()
    {
        FileHelper.ReadFromFile("Day1", "Measurements.txt");
        Assert.Equal(1690020, Day2Puzzle.Part1CalculateProductOfFinalPositionAndDepth(RealData));
    }

    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(900, Day2Puzzle.Part2CalculateProductOfFinalPositionAndDepth(SampleData));
    }

    [Fact]
    public void SolvePart2()
    {
        FileHelper.ReadFromFile("Day1", "Measurements.txt");
        Assert.Equal(1408487760, Day2Puzzle.Part2CalculateProductOfFinalPositionAndDepth(RealData));
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