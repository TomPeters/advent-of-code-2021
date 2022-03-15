using System.Collections;
using System.Collections.Generic;
using AdventOfCode;
using Xunit;

namespace AdventOfCodeTests.Day5;

public class Day5
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(0, Day5Puzzle.NumberOfOverlappingPoints(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(0, Day5Puzzle.NumberOfOverlappingPoints(RealData));
    }

    static IEnumerable<string> RealData => FileHelper.ReadFromFile("Day4", "VentLines.txt").Split("\n");

    static IEnumerable<string> SampleData => FileHelper.ReadFromFile("Day4", "SampleData.txt").Split("\n");
}