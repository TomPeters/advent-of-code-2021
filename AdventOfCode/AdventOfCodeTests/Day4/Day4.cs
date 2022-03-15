using System.Collections.Generic;
using AdventOfCode.Day3;
using AdventOfCode.Day4;
using Xunit;

namespace AdventOfCodeTests.Day4;

public class Day4
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(4512, Day4Puzzle.GetScoreOfWinningBoard(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(0, Day4Puzzle.GetScoreOfWinningBoard(RealData));
    }

    static IEnumerable<string> RealData => FileHelper.ReadFromFile("Day4", "RealBingoData.txt").Split("\n");

    static IEnumerable<string> SampleData => FileHelper.ReadFromFile("Day4", "SampleData.txt").Split("\n");
}