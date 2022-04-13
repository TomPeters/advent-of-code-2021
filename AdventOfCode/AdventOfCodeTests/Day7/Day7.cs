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
        Assert.Equal(1, Day7Puzzle.GetFuelToAlignToPosition());
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day7Puzzle.GetFuelToAlignToPosition());
    }

    static IEnumerable<int> RealData => FileHelper.ReadFromFile("Day7", "HorizontalPositions.txt").Split(",").Select(int.Parse);

    static IEnumerable<int> SampleData => new [] { 16,1,2,0,4,2,7,1,2,14 };
}