using System;
using AdventOfCode.Day3;
using Xunit;

namespace AdventOfCodeTests.Day3;

public class Day3
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(198, Day3Puzzle.GetSubmarinePowerConsumption(SampleData));
    }

    static readonly string[] SampleData = {
        "00100",
        "11110",
        "10110",
        "10111",
        "10101",
        "01111",
        "00111",
        "11100",
        "10000",
        "11001",
        "00010",
        "01010"
    };
}