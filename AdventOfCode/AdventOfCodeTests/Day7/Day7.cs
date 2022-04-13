using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day7;
using Xunit;

namespace AdventOfCodeTests.Day7;

public class Day7
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(37, Day7Puzzle.GetFuelToAlignToPosition(SampleData.ToArray(), new ConstantFuelCostCalculator()));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(328187, Day7Puzzle.GetFuelToAlignToPosition(RealData.ToArray(), new ConstantFuelCostCalculator()));
    }

    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(168, Day7Puzzle.GetFuelToAlignToPosition(SampleData.ToArray(), new IncrementingFuelCostCalculator()));
    }

    [Fact]
    public void Part2WorksForRealData()
    {
        Assert.Equal(91257582, Day7Puzzle.GetFuelToAlignToPosition(RealData.ToArray(), new IncrementingFuelCostCalculator()));
    }


    static IEnumerable<int> RealData => FileHelper.ReadFromFile("Day7", "HorizontalPositions.txt").Split(",").Select(int.Parse);

    static IEnumerable<int> SampleData => new [] { 16,1,2,0,4,2,7,1,2,14 };
}