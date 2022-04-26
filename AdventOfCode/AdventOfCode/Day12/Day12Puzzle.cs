using AdventOfCode.Utils;

namespace AdventOfCode.Day12;

public static class Day12Puzzle
{
    public static int GetNumberOfValidPaths(CaveNetwork caveNetwork, IPathValidator pathValidator)
    {
        return caveNetwork.GetAllValidPaths(pathValidator).Count();
    }
}

public class CaveNetwork
{
    readonly HashSet<Cave> _caves = new();
    public static CaveNetwork CreateCaveNetwork(IEnumerable<ConnectedCavePair> cavePair)
    {
        var caveNetwork = new CaveNetwork();
        cavePair.ForEach(caveNetwork.AddCavePairToNetwork);
        return caveNetwork;
    }

    void AddCavePairToNetwork(ConnectedCavePair pair)
    {
        var leftCave = AddCave(pair.LeftCave);
        var rightCave = AddCave(pair.RightCave);
        leftCave.ConnectToCave(rightCave);
    }

    Cave AddCave(Cave cave)
    {
        if (_caves.TryGetValue(cave, out var actualCave))
        {
            return actualCave;
        }

        _caves.Add(cave);
        return cave;
    }

    public IEnumerable<Path> GetAllValidPaths(IPathValidator pathValidator)
    {
        var startCave = GetStartingCave();
        return startCave.GetAllValidPathsToEndCave(pathValidator);
    }

    Cave GetStartingCave()
    {
        if (!_caves.TryGetValue(Cave.StartCave, out var startCave))
        {
            throw new Exception("No start cave added to network");
        }

        return startCave;
    }
}

public class Cave
{
    readonly string _caveId;
    readonly HashSet<Cave> _connectedCaves = new();

    public Cave(string caveId)
    {
        _caveId = caveId;
    }

    public bool IsSmallCave => !IsUpperCase(_caveId);

    public static Cave StartCave => new("start");
    static Cave EndCave => new Cave("end");
    public bool IsStartCave => Equals(StartCave);
    public bool IsEndCave => Equals(EndCave);

    static bool IsUpperCase(string input) => input.ToUpperInvariant() == input;

    public void ConnectToCave(Cave otherCave)
    {
        _connectedCaves.Add(otherCave);
        otherCave._connectedCaves.Add(this);
    }

    public IEnumerable<Path> GetAllValidPathsToEndCave(IPathValidator pathValidator)
    {
        return GetAllValidPathsToEndCave(Path.From(this), pathValidator);
    }

    IEnumerable<Path> GetAllValidPathsToEndCave(Path pathSoFarIncludingThisCave, IPathValidator pathValidator)
    {
        if (IsEndCave)
        {
            return new [] { pathSoFarIncludingThisCave };
        }

        return _connectedCaves.SelectMany(c =>
        {
            var pathWithConnectedCave = pathSoFarIncludingThisCave.Add(c);
            return !pathValidator.IsValidPath(pathWithConnectedCave) 
                ? Enumerable.Empty<Path>()
                : c.GetAllValidPathsToEndCave(pathWithConnectedCave, pathValidator);
        }).ToArray();
    }
    
    protected bool Equals(Cave other)
    {
        return _caveId == other._caveId;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Cave)obj);
    }

    public override int GetHashCode()
    {
        return _caveId.GetHashCode();
    }
}

public class Path
{
    public IEnumerable<Cave> CavesInPath { get; }

    public Path(IEnumerable<Cave> cavesInPath)
    {
        CavesInPath = cavesInPath;
    }

    public static Path From(Cave cave)
    {
        return new Path(new[] { cave });
    }

    public Path Add(Cave cave)
    {
        return new Path(CavesInPath.Concat(new[] { cave }).ToArray());
    }
};

public interface IPathValidator
{
    public bool IsValidPath(Path path);
}

public class VisitsSmallCavesAtMostOncePathValidator : IPathValidator
{
    public bool IsValidPath(Path path)
    {
        return !path.CavesInPath
            .GroupBy(cave => cave)
            .Where(cave => cave.Key.IsSmallCave)
            .Any(caveVisits => caveVisits.Count() > 1);
    }
}

public class Part2PathValidator : IPathValidator
{
    public bool IsValidPath(Path path)
    {
        var smallCaveGroups = path.CavesInPath
            .GroupBy(cave => cave)
            .Where(cave => cave.Key.IsSmallCave)
            .ToList();
        
        var smallCavesVisitedMoreThanTwice = smallCaveGroups
            .Any(caveGroup => caveGroup.Count() > 2);
        
        var numberOfSmallCavesVisitedTwice = smallCaveGroups
            .Count(caveGroup => caveGroup.Count() == 2);

        var multipleVisitsToStartOrEndCaves = smallCaveGroups
            .Where(caveGroup => caveGroup.Key.IsEndCave || caveGroup.Key.IsStartCave)
            .Any(caveGroup => caveGroup.Count() > 1);

        return !smallCavesVisitedMoreThanTwice && numberOfSmallCavesVisitedTwice <= 1 && !multipleVisitsToStartOrEndCaves;
    }
}

public class ConnectedCavePair
{
    readonly string _leftCaveId;
    readonly string _rightCaveId;

    public ConnectedCavePair(string leftCaveId, string rightCaveId)
    {
        _leftCaveId = leftCaveId;
        _rightCaveId = rightCaveId;
    }

    public Cave LeftCave => new(_leftCaveId);
    public Cave RightCave => new(_rightCaveId);
}