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
        ConnectHorizontallyAdjacentLocations(locationMap);
        ConnectVerticallyAdjacentLocations(locationMap);
        return new HeightMap(locationMap.Flatten());
    }

    static void ConnectVerticallyAdjacentLocations(List<List<Location>> locationMap)
    {
        locationMap.Zip(locationMap.Skip(1), (row1, row2) => row1.Zip(row2, (top, bottom) => (top, bottom)))
            .Flatten()
            .ForEach(verticalPair => verticalPair.top.ConnectToAdjacentLocation(verticalPair.bottom));
    }

    static void ConnectHorizontallyAdjacentLocations(List<List<Location>> locationMap)
    {
        locationMap.ForEach(locationRow =>
        {
            locationRow.Zip(locationRow.Skip(1), (left, right) => (left, right))
                .ForEach(horizontalPair => horizontalPair.left.ConnectToAdjacentLocation(horizontalPair.right));
        });
    }
}

public class Location
{
    readonly int _height;
    readonly List<Location> _adjacentLocations = new List<Location>();

    public Location(int height)
    {
        _height = height;
    }

    public void ConnectToAdjacentLocation(Location location)
    {
        _adjacentLocations.Add(location);
        location._adjacentLocations.Add(this);
    }

    public int RiskLevel => _height + 1;
    public bool IsLowPoint => _adjacentLocations.Select(h => h._height).All(h => h > _height);
}