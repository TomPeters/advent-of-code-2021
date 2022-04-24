using AdventOfCode.Day10;
using AdventOfCode.Day11;
using Xunit;

namespace AdventOfCodeTests.Day11;

public class Day11
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(1656, Day11Puzzle.GetNumberOfFlashes(SampleData, 100));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day11Puzzle.GetNumberOfFlashes(RealData, 100));
    }
    
    static string RealData => FileHelper.ReadFromFile("Day11", "SampleEnergyLevels.txt");

    static string SampleData => FileHelper.ReadFromFile("Day11", "RealEnergyLevels.txt");
}