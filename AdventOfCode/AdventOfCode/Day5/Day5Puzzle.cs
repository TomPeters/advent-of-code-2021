using AdventOfCode.Utils;

namespace AdventOfCode.Day5;

public static class Day5Puzzle
{
    public static int NumberOfOverlappingPointsExcludingDiagonals(IEnumerable<Line> ventLines)
    {
        return NumberOfOverlappingPoints(ventLines.Where(l => !l.IsDiagonal()));
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

    public bool IsDiagonal() => !IsHorizontalLine() && !IsVerticalLine();
    bool IsHorizontalLine() => _lineEndpoints.Start.Y == _lineEndpoints.End.Y;
    bool IsVerticalLine() => _lineEndpoints.Start.X == _lineEndpoints.End.X;

    public IEnumerable<Coordinate> GetAllCoordinatesAlongLine()
    {
        if (IsDiagonal())
        {
            // This uses the property that the size of the X and Y ranges are identical because the lines are at 45 degrees
            // Because we know the ranges are identical, we can zip them #hackz
            return EnumerableExtensions.BidirectionalRange(_lineEndpoints.Start.X, _lineEndpoints.End.X)
                .Zip(EnumerableExtensions.BidirectionalRange(_lineEndpoints.Start.Y, _lineEndpoints.End.Y))
                .Select(xAndY => new Coordinate(xAndY.First, xAndY.Second));
        }

        return EnumerableExtensions.BidirectionalRange(_lineEndpoints.Start.X, _lineEndpoints.End.X)
            .SelectMany(x => EnumerableExtensions.BidirectionalRange(_lineEndpoints.Start.Y, _lineEndpoints.End.Y).Select(y => new Coordinate(x, y)));
    }
}

public record LineEndpoints(Coordinate Start, Coordinate End);

public record Coordinate(int X, int Y);