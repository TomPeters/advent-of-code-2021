using System.Linq;
using AdventOfCode.Day13;
using Xunit;

namespace AdventOfCodeTests.Day13;

public class Day13
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(1, Day13Puzzle.NumberOfDotsVisibleAfterFirstFold(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day13Puzzle.NumberOfDotsVisibleAfterFirstFold(RealData));
    }
    
    static Day13Input RealData => CreateInput(FileHelper.ReadFromFile("Day13", "TransparentPaper.txt"));

    static Day13Input SampleData => CreateInput(@"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5");

    static Day13Input CreateInput(string input)
    {
        var inputSections = input.Split("\n\n");
        var dotsSection = inputSections[0];
        var foldsSection = inputSections[1];
        var dots = dotsSection.Split("\n").Select(dotLine =>
        {
            var coords = dotLine.Split(",").Select(int.Parse).ToArray();
            var x = coords[0];
            var y = coords[1];
            return new Dot(x, y);
        }).ToArray();
        var folds = foldsSection.Split("\n").Select(foldLine =>
        {
            var foldSpec = foldLine.Split("fold along ")[1];
            var foldSpecParts = foldSpec.Split("=");
            var foldAxis = foldSpecParts[0] == "y" ? FoldAxis.Y : FoldAxis.X;
            var foldValue = int.Parse(foldSpecParts[1]);
            return new FoldLine(foldAxis, foldValue);
        }).ToArray();
        return new Day13Input(dots, folds);
    }
}