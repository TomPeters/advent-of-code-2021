using AdventOfCode.Utils;

namespace AdventOfCode.Day12;

public static class Day12Puzzle
{
    public static int GetNumberOfPathsThatVisitSmallCavesAtMostOnce(CaveNetwork caveNetwork)
    {
        return 0;
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
}

public class Cave
{
    readonly string _caveId;
    public HashSet<Cave> _connectedCaves = new();

    public Cave(string caveId)
    {
        _caveId = caveId;
    }

    public bool IsBigCave => IsUpperCase(_caveId);

    public bool IsStartCave => _caveId == "start";
    public bool IsEndCave => _caveId == "end";

    static bool IsUpperCase(string input) => input.ToUpperInvariant() == input;

    public void ConnectToCave(Cave otherCave)
    {
        _connectedCaves.Add(otherCave);
        otherCave._connectedCaves.Add(this);
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

public class ConnectedCavePair
{
    readonly string _leftCaveId;
    readonly string _rightCaveId;

    public ConnectedCavePair(string leftCaveId, string rightCaveId)
    {
        _leftCaveId = leftCaveId;
        _rightCaveId = rightCaveId;
    }

    public Cave LeftCave => new Cave(_leftCaveId);
    public Cave RightCave => new Cave(_rightCaveId);

    public IEnumerable<Cave> Caves => new[] { new Cave(_leftCaveId), new Cave(_rightCaveId) };
}