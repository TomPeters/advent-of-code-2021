using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Day3;
using AdventOfCode.Day4;
using AdventOfCode.Utils;
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
        Assert.Equal(10374, Day4Puzzle.GetScoreOfWinningBoard(RealData));
    }

    static IBingoPuzzleInput RealData => new Day4Input(FileHelper.ReadFromFile("Day4", "RealBingoData.txt").Split("\n"));

    static IBingoPuzzleInput SampleData => new Day4Input(FileHelper.ReadFromFile("Day4", "SampleData.txt").Split("\n"));
}

public class Day4Input : IBingoPuzzleInput
{
    readonly string[] _inputLines;

    public Day4Input(IEnumerable<string> inputLines)
    {
        _inputLines = inputLines.ToArray();
    }

    public IEnumerable<int> DrawnNumbers => _inputLines.First().Split(",").Select(int.Parse);

    public IEnumerable<BingoBoard> Boards
    {
        get
        {
            return _inputLines.Skip(2).Batch(6).Select(bingoLines =>
            {
                var rows = bingoLines.Take(5).Select(l =>
                {
                    var strings = l.Split(" ").Where(s => s != "");
                    return strings.Select(int.Parse);
                });
                return new BingoBoard(rows);
            });
        }
    }
}