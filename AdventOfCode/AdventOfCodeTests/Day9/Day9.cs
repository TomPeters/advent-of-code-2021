using AdventOfCode.Day9;
using Xunit;

namespace AdventOfCodeTests.Day9;

public class Day9
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(15, Day9Puzzle.GetSumOfRiskLevelsOfLowPoints(new HeightMap(SampleData)));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day9Puzzle.GetSumOfRiskLevelsOfLowPoints(new HeightMap(RealData)));
    }

    static string RealData => FileHelper.ReadFromFile("Day7", "HorizontalPositions.txt");

    static string SampleData => @"2199943210
3987894921
9856789892
8767896789
9899965678";
}