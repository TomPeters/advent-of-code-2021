using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day9;
using Xunit;

namespace AdventOfCodeTests.Day9;

public class Day9
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(15, Day9Puzzle.GetSumOfRiskLevelsOfLowPoints(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(436, Day9Puzzle.GetSumOfRiskLevelsOfLowPoints(RealData));
    }

    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(1134, Day9Puzzle.GetProductOfThreeLargestBasins(SampleData));
    }

    [Fact]
    public void Part2WorksForRealData()
    {
        Assert.Equal(1317792, Day9Puzzle.GetProductOfThreeLargestBasins(RealData));
    }

    static HeightMap RealData => ConvertToHeightMap(FileHelper.ReadFromFile("Day9", "HeightMap.txt"));

    static HeightMap SampleData => ConvertToHeightMap(@"2199943210
3987894921
9856789892
8767896789
9899965678");

    static HeightMap ConvertToHeightMap(string input)
    {
        int[][] heights = input.Split("\n").Select(r => r.ToCharArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
        return HeightMap.Create(heights);
    }
}