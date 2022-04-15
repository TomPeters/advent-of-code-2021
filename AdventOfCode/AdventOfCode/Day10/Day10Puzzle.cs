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

    public static long GetMiddleScoreOfCompletionStringsOfIncompleteLines(NavigationSubsystem navigationSubsystem)
    {
        return navigationSubsystem.Lines
            .Where(l => l.IsIncomplete())
            .Select(l => l.GetCompletionString())
            .Select(cs => cs.GetScore())
            .OrderBy(score => score)
            .Middle();
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

    // After discarding corrupted lines, the remaining lines are incomplete.
    public bool IsIncomplete() => !IsCorrupted();
    bool IsCorrupted() => GetFirstIncorrectClosingSymbol() != null;

    public CompletionString GetCompletionString()
    {
        var stack = new Stack<ChunkType>();
        foreach (var symbol in _chunkSymbols)
        {
            if (symbol.IsEnd())
            {
                var poppedChunkType = stack.Pop();

                if (!symbol.IsSameChunkType(poppedChunkType))
                {
                    throw new Exception("Expected symbol to match current chunk type");
                }
            }
            else
            {
                stack.Push(symbol.ChunkType);
            }
        }

        var completionSymbols = stack.Select(chunkType => new Symbol(chunkType, Side.End));
        return new CompletionString(completionSymbols);

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

public class CompletionString
{
    readonly IEnumerable<Symbol> _completionSymbols;

    public CompletionString(IEnumerable<Symbol> completionSymbols)
    {
        _completionSymbols = completionSymbols;
    }

    public long GetScore()
    {
        return _completionSymbols.Aggregate(0L, (prev, cur) =>
        {
            var multipliedScore = prev * 5;
            return multipliedScore + cur.GetCompletionCharacterScore();
        });
    }
}

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

    public int GetCompletionCharacterScore()
    {
        return ChunkType switch
        {
            ChunkType.Brace => 3,
            ChunkType.SquareBracket => 2,
            ChunkType.Parens => 1,
            ChunkType.AngleBracket => 4,
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