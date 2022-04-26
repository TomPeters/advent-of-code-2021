namespace AdventOfCode.Day13;

public static class Day13Puzzle
{
    public static int NumberOfDotsVisibleAfterFirstFold(Day13Input input)
    {
        return 0;
    }
}

public record Day13Input(Dot[] Dots, FoldLine[] Folds);

public record Dot(int X, int Y);

public record FoldLine(FoldAxis FoldAxis, int FoldValue);

public enum FoldAxis
{
    X,
    Y
}