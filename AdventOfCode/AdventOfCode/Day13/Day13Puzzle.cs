using AdventOfCode.Utils;

namespace AdventOfCode.Day13;

public static class Day13Puzzle
{
    public static int NumberOfDotsVisibleAfterFirstFold(Day13Input input)
    {
        var paper = new TransparentPaper(input.Dots);
        paper.Fold(input.Folds[0]);
        return paper.Dots.Length;
    }
    
    public static IEnumerable<string> ApplyAllFoldsAndPrintResult(Day13Input input)
    {
        var paper = new TransparentPaper(input.Dots);
        input.Folds.ForEach(fold => paper.Fold(fold));
        return paper.PrintLayout();
    }
}

public class TransparentPaper
{
    public TransparentPaper(Dot[] dots)
    {
        Dots = dots;
    }

    public Dot[] Dots { get; private set; }

    public IEnumerable<string> PrintLayout()
    {
        var maxX = Dot.MaxX(Dots);
        var maxY = Dot.MaxY(Dots);

        return EnumerableExtensions.BidirectionalRange(0, maxY).Select(yCoord =>
        {
            return string.Join("", EnumerableExtensions.BidirectionalRange(0, maxX).Select(xCoord =>
            {
                var dotAtCurrentCoord = new Dot(xCoord, yCoord);
                if (Dots.Any(d => d.Equals(dotAtCurrentCoord)))
                {
                    return "#";
                }

                return ".";
            }));
        });
    }

    public void Fold(FoldLine foldLine)
    {
        Dots = Dots.Select(d => d.FoldDot(foldLine)).Distinct().ToArray();
    }
}

public record Day13Input(Dot[] Dots, FoldLine[] Folds);

public class Dot
{
    readonly int _x;
    readonly int _y;

    public Dot(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public static int MaxX(IEnumerable<Dot> dots)
    {
        return dots.Max(d => d._x);
    }

    public static int MaxY(IEnumerable<Dot> dots)
    {
        return dots.Max(d => d._y);
    }
    
    public Dot FoldDot(FoldLine foldLine)
    {
        if (foldLine.FoldAxis == FoldAxis.X)
        {
            var newXPosition = GetAdjustedCoordinate(_x, foldLine.FoldValue);
            return new Dot(newXPosition, _y);
        }

        var newYPosition = GetAdjustedCoordinate(_y, foldLine.FoldValue);
        return new Dot(_x, newYPosition);
    }

    static int GetAdjustedCoordinate(int originalCoord, int foldCoordinate)
    {
        var displacementToFoldLine = originalCoord - foldCoordinate;
        var willNotBeFolded = displacementToFoldLine <= 0;
        if (willNotBeFolded)
        {
            return originalCoord;
        }

        var foldedCoordinate = originalCoord - displacementToFoldLine * 2;
        return foldedCoordinate;
    }

    protected bool Equals(Dot other)
    {
        return _x == other._x && _y == other._y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Dot)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_x, _y);
    }
};

public record FoldLine(FoldAxis FoldAxis, int FoldValue);

public enum FoldAxis
{
    X,
    Y
}