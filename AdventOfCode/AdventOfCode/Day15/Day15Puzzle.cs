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

    Cave(CaveLocation startLocation, CaveLocation endLocation)
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
        var seenLocations = new HashSet<CaveLocation>();
        var incompletePathsByRisk = new PriorityQueue<Path, int>();
        var startingPath = new Path(new [] { _startLocation });
        incompletePathsByRisk.Enqueue(startingPath, startingPath.Risk);

        while (incompletePathsByRisk.Count > 0)
        {
            var incompletePath = incompletePathsByRisk.Dequeue();
            var incompletePathEndOfPath = incompletePath.EndOfPath;
            seenLocations.Add(incompletePathEndOfPath);
            var newPaths = incompletePathEndOfPath.AdjacentLocations
                .Where(l => !seenLocations.Contains(l))
                .Select(newLocation => incompletePath.AddNewLocation(newLocation));
            
            foreach (var path in newPaths)
            {
                if (path.EndOfPath == _endLocation)
                {
                    yield return path;
                }
                else
                {
                    incompletePathsByRisk.Enqueue(path, path.Risk);
                }
            }
        }
    }
}

public class CaveLocation
{
    public int RiskLevel { get; }
    readonly HashSet<CaveLocation> _adjacentLocations = new ();

    public CaveLocation(int riskLevel)
    {
        RiskLevel = riskLevel;
    }

    public void ConnectToAdjacentLocation(CaveLocation location)
    {
        _adjacentLocations.Add(location);
        location._adjacentLocations.Add(this);
    }

    public IEnumerable<CaveLocation> AdjacentLocations => _adjacentLocations;
}

public class Path
{
    readonly CaveLocation[] _locations;

    public Path(IEnumerable<CaveLocation> locations)
    {
        _locations = locations.ToArray();
    }

    public CaveLocation EndOfPath => _locations.Last();

    public Path AddNewLocation(CaveLocation location)
    {
        return new Path(_locations.Concat(new[] { location }));
    }

    public int Risk => _locations.Skip(1).Sum(l => l.RiskLevel);
}