using AdventOfCode.Utils;

namespace AdventOfCode;

public class Day5Puzzle
{
    public static int NumberOfOverlappingPointsExcludingDiagonals(IEnumerable<Line> ventLines)
    {
        return NumberOfOverlappingPoints(ventLines.Where(l => l.IsHorizontalLine() || l.IsVerticalLine()));
    }

    public static int NumberOfOverlappingPoints(IEnumerable<Line> ventLines)
    {
        var allCoordinatesIncludingDuplicates = ventLines.SelectMany(l => l.GetAllCoordinatesAlongLine()).ToArray();
        return allCoordinatesIncludingDuplicates.GroupBy(c => c).Count(c => c.Count() > 1);
    }
}

public class Line
{
    readonly LineEndpoints _lineEndpoints;

    public Line(LineEndpoints lineEndpoints)
    {
        _lineEndpoints = lineEndpoints;
    }

    public bool IsHorizontalLine()
    {
        return _lineEndpoints.Start.Y == _lineEndpoints.End.Y;
    }

    public bool IsVerticalLine()
    {
        return _lineEndpoints.Start.X == _lineEndpoints.End.X;
    }

    public IEnumerable<Coordinate> GetAllCoordinatesAlongLine()
    {
        foreach (var x in EnumerableExtensions.BidirectionalRange(_lineEndpoints.Start.X, _lineEndpoints.End.X))
        {
            foreach (var y in EnumerableExtensions.BidirectionalRange(_lineEndpoints.Start.Y, _lineEndpoints.End.Y))
            {
                yield return new Coordinate(x, y);
            }
        }
    }
}

public record LineEndpoints(Coordinate Start, Coordinate End);

public record Coordinate(int X, int Y);