using AdventOfCode.Utils;

namespace AdventOfCode.Day10;

public static class Day10Puzzle
{
    public static int GetTotalSyntaxErrorScoreForFirstErrorInLine(NavigationSubsystem navigationSubsystem)
    {
        return navigationSubsystem.Lines
            .Select(l => l.GetFirstIncorrectClosingSymbol())
            .NotNull()
            .Sum(s => s.GetIllegalCharacterScore());
    }
}

public record NavigationSubsystem(IEnumerable<NavigationSubsystemLine> Lines);

public class NavigationSubsystemLine
{
    readonly IEnumerable<Symbol> _chunkSymbols;

    public NavigationSubsystemLine(IEnumerable<Symbol> chunkSymbols)
    {
        _chunkSymbols = chunkSymbols;
    }

    public Symbol? GetFirstIncorrectClosingSymbol()
    {
        var stack = new Stack<ChunkType>();
        foreach (var symbol in _chunkSymbols)
        {
            if (symbol.IsEnd())
            {
                var currentChunk = stack.Pop();
                var symbolClosedTheWrongChunkType = !symbol.IsSameChunkType(currentChunk);
                if (symbolClosedTheWrongChunkType)
                {
                    return symbol;
                }
            }
            else
            {
                stack.Push(symbol.ChunkType);
            }
        }

        return null;
    }
};

public class Symbol
{
    readonly Side _side;

    public Symbol(ChunkType chunkType, Side side)
    {
        ChunkType = chunkType;
        _side = side;
    }

    public ChunkType ChunkType { get; }

    public bool IsEnd()
    {
        return _side == Side.End;
    }

    public bool IsSameChunkType(ChunkType otherChunkType)
    {
        return otherChunkType == ChunkType;
    }

    public int GetIllegalCharacterScore()
    {
        return ChunkType switch
        {
            ChunkType.Brace => 1197,
            ChunkType.SquareBracket => 57,
            ChunkType.Parens => 3,
            ChunkType.AngleBracket => 25137,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum Side
{
    Start,
    End
}

public enum ChunkType
{
    SquareBracket,
    Parens,
    AngleBracket,
    Brace
}