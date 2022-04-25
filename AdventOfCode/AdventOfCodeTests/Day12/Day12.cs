using AdventOfCode.Day12;
using Xunit;

namespace AdventOfCodeTests.Day12;

public class Day12
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(226, Day12Puzzle.GetNumberOfPathsThatVisitSmallCavesAtMostOnce(SampleData));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1, Day12Puzzle.GetNumberOfPathsThatVisitSmallCavesAtMostOnce(RealData));
    }
    
    static CaveNetwork RealData => CreateCaveNetwork(FileHelper.ReadFromFile("Day12", "RealCaveNetworkConnections.txt"));

    static CaveNetwork SampleData => CreateCaveNetwork(FileHelper.ReadFromFile("Day12", "SampleCaveNetworkConnections.txt"));

    static CaveNetwork CreateCaveNetwork(string input)
    {
        return CaveNetwork.CreateCaveNetwork(input);
    }
}