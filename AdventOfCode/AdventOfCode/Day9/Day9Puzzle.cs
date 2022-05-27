using AdventOfCode.Utils;

namespace AdventOfCode.Day9;

public static class Day9Puzzle
{
    public static int GetSumOfRiskLevelsOfLowPoints(HeightMap heightMap)
    {
        return heightMap.Locations
            .Where(h => h.IsLowPoint)
            .Sum(h => h.RiskLevel);
    }
    
    public static int GetProductOfThreeLargestBasins(HeightMap heightMap)
    {
        return heightMap.Basins
            .Select(b => b.Size)
            .OrderByDescending(basinSize => basinSize)
            .Take(3)
            .Product();
    }
}

public class HeightMap
{
    public Location[] Locations { get; }

    HeightMap(IEnumerable<Location> heights)
    {
        Locations = heights.ToArray();
    }

    public static HeightMap Create(int[][] heights)
    {
        var locationMap = heights.Select(row => row.Select(height => new Location(height)).ToList()).ToList();
        var connectedPairs = GridConnections.GetAdjacentLocations(locationMap);
        connectedPairs.ForEach(pair => pair.First.ConnectToAdjacentLocation(pair.Second));
        return new HeightMap(locationMap.Flatten());
    }

    public IEnumerable<Basin> Basins
    {
        get
        {
            return Locations
                .Select(l => l.GetContainingBasin())
                .NotNull()
                .Distinct();
        }
    }
}

public class Location
{
    readonly int _height;
    readonly List<Location> _adjacentLocations = new();

    public Location(int height)
    {
        _height = height;
    }

    public void ConnectToAdjacentLocation(Location location)
    {
        _adjacentLocations.Add(location);
        location._adjacentLocations.Add(this);
    }

    public Basin? GetContainingBasin()
    {
        if (!IsInBasin()) return null;
        var seenLocations = new HashSet<Location>() { this };
        PopulateSeenLocationsWithConnectedLocationsInBasin(seenLocations);
        return new Basin(seenLocations);
    }

    void PopulateSeenLocationsWithConnectedLocationsInBasin(ISet<Location> seenLocations)
    {
        var unseenAdjacentLocations = GetUnseenAdjacentLocationsInBasin(seenLocations).ToArray();
        unseenAdjacentLocations.ForEach(l => seenLocations.Add(l));
        unseenAdjacentLocations.ForEach(l => l.PopulateSeenLocationsWithConnectedLocationsInBasin(seenLocations));
    }

    IEnumerable<Location> GetUnseenAdjacentLocationsInBasin(ISet<Location> seenLocations)
    {
        return _adjacentLocations.Where(l => l.IsInBasin() && !seenLocations.Contains(l));
    }

    bool IsInBasin() => _height != 9;

    public int RiskLevel => _height + 1;
    public bool IsLowPoint => _adjacentLocations.Select(h => h._height).All(h => h > _height);
}

public class Basin
{
    readonly ISet<Location> _locations;

    public Basin(ISet<Location> locations)
    {
        _locations = locations;
    }

    public int Size => _locations.Count();

    protected bool Equals(Basin other)
    {
        return _locations.SetEquals(other._locations);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Basin)obj);
    }

    public override int GetHashCode()
    {
        return _locations.Aggregate(1, (prev, cur) => prev ^ cur.GetHashCode());
    }
}