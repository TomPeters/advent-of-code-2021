using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day6;
using Xunit;

namespace AdventOfCodeTests.Day6;

public class Day6
{
    [Fact]
    public void Part1WorksForSampleDataFor18Days()
    {
        Assert.Equal(26, Day6Puzzle.NumberOfLanternFishAfterDays(SampleData, 18));
    }

    [Fact]
    public void Part1WorksForSampleDataFor80Days()
    {
        Assert.Equal(5934, Day6Puzzle.NumberOfLanternFishAfterDays(SampleData, 80));
    }

    [Fact]
    public void Part1WorksForRealDataFor80Days()
    {
        Assert.Equal(0, Day6Puzzle.NumberOfLanternFishAfterDays(RealData, 80));
    }

    static IEnumerable<int> RealData => FileHelper.ReadFromFile("Day5", "VentLines.txt").Split(",").Select(int.Parse);

    static IEnumerable<int> SampleData => new [] {3,4,3,1,2};
}