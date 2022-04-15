using System;
using System.Linq;
using AdventOfCode.Day10;
using Xunit;

namespace AdventOfCodeTests.Day10;

public class Day10
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(26397, Day10Puzzle.GetTotalSyntaxErrorScoreForFirstErrorInLine(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(167379, Day10Puzzle.GetTotalSyntaxErrorScoreForFirstErrorInLine(RealData));
    }

    static NavigationSubsystem RealData => CreateNavigationSubsystemFromInput(FileHelper.ReadFromFile("Day10", "RealNavigationSubsystem.txt"));

    static NavigationSubsystem SampleData => CreateNavigationSubsystemFromInput(FileHelper.ReadFromFile("Day10", "SampleNavigationSubsystem.txt"));

    static NavigationSubsystem CreateNavigationSubsystemFromInput(string input)
    {
        return new NavigationSubsystem(input.Split("\n").Select(l =>
        {
            var symbols = l.Select(c =>
            {
                return c switch
                {
                    '<' => new Symbol(ChunkType.AngleBracket, Side.Start),
                    '>' => new Symbol(ChunkType.AngleBracket, Side.End),
                    '(' => new Symbol(ChunkType.Parens, Side.Start),
                    ')' => new Symbol(ChunkType.Parens, Side.End),
                    '[' => new Symbol(ChunkType.SquareBracket, Side.Start),
                    ']' => new Symbol(ChunkType.SquareBracket, Side.End),
                    '{' => new Symbol(ChunkType.Brace, Side.Start),
                    '}' => new Symbol(ChunkType.Brace, Side.End),
                    _ => throw new ArgumentOutOfRangeException(nameof(c))
                };
            });
            return new NavigationSubsystemLine(symbols);
        }));
    }
}
