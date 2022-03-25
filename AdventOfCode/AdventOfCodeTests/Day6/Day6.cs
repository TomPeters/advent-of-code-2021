using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day6;
using Xunit;

namespace AdventOfCodeTests.Day6;

public class Day6
{
    [Fact]
    public void WorksForSampleDataFor18Days()
    {
        Assert.Equal(26, Day6Puzzle.NumberOfLanternFishAfterDays(SampleData, 18));
    }

    [Fact]
    public void WorksForSampleDataFor80Days()
    {
        Assert.Equal(5934, Day6Puzzle.NumberOfLanternFishAfterDays(SampleData, 80));
    }

    [Fact]
    public void WorksForSampleDataFor256Days()
    {
        Assert.Equal(26984457539, Day6Puzzle.NumberOfLanternFishAfterDays(SampleData, 256));
    }

    [Fact]
    public void WorksForRealDataFor80Days()
    {
        Assert.Equal(391888, Day6Puzzle.NumberOfLanternFishAfterDays(RealData, 80));
    }

    [Fact]
    public void WorksForRealDataFor256Days()
    {
        Assert.Equal(1754597645339, Day6Puzzle.NumberOfLanternFishAfterDays(RealData, 256));
    }

    static IEnumerable<int> RealData => FileHelper.ReadFromFile("Day6", "LanternfishInitialTimers.txt").Split(",").Select(int.Parse);

    static IEnumerable<int> SampleData => new [] {3,4,3,1,2};
}