using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using Xunit;

namespace AdventOfCodeTests.Day5;

public class Day5
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(5, Day5Puzzle.NumberOfOverlappingPointsExcludingDiagonals(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(5835, Day5Puzzle.NumberOfOverlappingPointsExcludingDiagonals(RealData));
    }

    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(12, Day5Puzzle.NumberOfOverlappingPoints(SampleData));
    }

    [Fact]
    public void Part2WorksForRealData()
    {
        Assert.Equal(5835, Day5Puzzle.NumberOfOverlappingPoints(RealData));
    }

    static IEnumerable<Line> RealData => ConvertInputToLines(FileHelper.ReadFromFile("Day5", "VentLines.txt").Split("\n"));

    static IEnumerable<Line> SampleData => ConvertInputToLines(FileHelper.ReadFromFile("Day5", "SampleData.txt").Split("\n"));

    static IEnumerable<Line> ConvertInputToLines(IEnumerable<string> inputLines)
    {
        return inputLines.Select(l =>
        {
            var parts = l.Split(" ").ToArray();
            var start = ParseCoordinate(parts.First());
            var end = ParseCoordinate(parts.Last());
            return new Line(new LineEndpoints(start, end));
        });
    }

    static Coordinate ParseCoordinate(string coordinateString)
    {
        var coordinateParts = coordinateString.Split(",").ToArray();
        return new Coordinate(int.Parse(coordinateParts.First()), int.Parse(coordinateParts.Last()));
    }
}