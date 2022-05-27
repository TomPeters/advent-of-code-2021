using System.Xml;
using AdventOfCode.Utils;

namespace AdventOfCode.Day15;

public static class Day15Puzzle
{
    public static int GetRiskOfLowestRiskPath(Cave cave)
    {
        return cave.GetPathsOrderedByRisk().First().Risk;
    }
}

public class Cave
{
    readonly CaveLocation _startLocation;
    readonly CaveLocation _endLocation;

    public Cave(CaveLocation startLocation, CaveLocation endLocation)
    {
        _startLocation = startLocation;
        _endLocation = endLocation;
    }

    public static Cave CreateCave(int[][] riskLevelMatrix)
    {
        var riskLevels = riskLevelMatrix.Select(rows => rows.Select(riskLevel => new CaveLocation(riskLevel)).ToArray()).ToArray();
        ConnectAdjacentLocations(riskLevels);
        var startingLocation = riskLevels.First().First();
        var endLocation = riskLevels.Last().Last();
        return new Cave(startingLocation, endLocation);
    }

    static void ConnectAdjacentLocations(CaveLocation[][] caveLocations)
    {
        var adjacentLocations = GridConnections.GetAdjacentLocations(caveLocations);
        adjacentLocations.ForEach(connectedPair => connectedPair.First.ConnectToAdjacentLocation(connectedPair.Second));
    }

    public IEnumerable<Path> GetPathsOrderedByRisk()
    {
        return new[] { new Path(new List<CaveLocation>() { _startLocation}) };
    }
}

public class CaveLocation
{
    public int RiskLevel { get; }
    HashSet<CaveLocation> _adjacentLocations = new ();

    public CaveLocation(int riskLevel)
    {
        RiskLevel = riskLevel;
    }

    public void ConnectToAdjacentLocation(CaveLocation location)
    {
        _adjacentLocations.Add(location);
        location._adjacentLocations.Add(this);
    }
}

public class Path
{
    readonly List<CaveLocation> _locations;

    public Path(List<CaveLocation> locations)
    {
        _locations = locations;
    }

    public int Risk => _locations.Sum(l => l.RiskLevel);
}