using System.Linq;
using AdventOfCode.Day12;
using Xunit;

namespace AdventOfCodeTests.Day12;

public class Day12
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(226, Day12Puzzle.GetNumberOfValidPaths(SampleData, new VisitsSmallCavesAtMostOncePathValidator()));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(5252, Day12Puzzle.GetNumberOfValidPaths(RealData, new VisitsSmallCavesAtMostOncePathValidator()));
    }
    
    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(3509, Day12Puzzle.GetNumberOfValidPaths(SampleData, new Part2PathValidator()));
    }

    [Fact]
    public void Part2WorksForRealData()
    {
        Assert.Equal(147784, Day12Puzzle.GetNumberOfValidPaths(RealData, new Part2PathValidator()));
    }
    
    static CaveNetwork RealData => CreateCaveNetwork(FileHelper.ReadFromFile("Day12", "RealCaveNetworkConnections.txt"));

    static CaveNetwork SampleData => CreateCaveNetwork(FileHelper.ReadFromFile("Day12", "SampleCaveNetworkConnections.txt"));

    static CaveNetwork CreateCaveNetwork(string input)
    {
        var connectionPairs = input.Split('\n').Select(connectionString =>
        {
            var connectionCaveIds = connectionString.Split("-");
            return new ConnectedCavePair(connectionCaveIds[0], connectionCaveIds[1]);
        });
        return CaveNetwork.CreateCaveNetwork(connectionPairs);
    }
}