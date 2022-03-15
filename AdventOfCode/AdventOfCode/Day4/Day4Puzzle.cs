namespace AdventOfCode.Day4;

public class Day4Puzzle
{
    public static int GetScoreOfFirstBoardToWin(IBingoPuzzleInput input)
    {
        return input.Boards.Select(b => b.Play(input.DrawnNumbers)).OrderBy(g => g.CountOfNumbersDrawnUntilWin).First().GetScore();
    }

    public static int GetScoreOfLastBoardToWin(IBingoPuzzleInput input)
    {
        return input.Boards.Select(b => b.Play(input.DrawnNumbers)).OrderBy(g => g.CountOfNumbersDrawnUntilWin).Last().GetScore();
    }
}

public interface IBingoPuzzleInput
{
    public IEnumerable<int> DrawnNumbers { get; }
    public IEnumerable<BingoBoard> Boards { get; }
}

public class BingoBoard
{
    readonly IEnumerable<IEnumerable<int>> _rows;

    public BingoBoard(IEnumerable<IEnumerable<int>> rows)
    {
        _rows = rows;
    }

    public BingoGameResult Play(IEnumerable<int> numbersToDraw)
    {
        var game = new BingoGame(_rows);
        return game.Play(numbersToDraw);
    }
}

public class BingoGame
{
    public BingoGame(IEnumerable<IEnumerable<int>> rows)
    {
        Rows = rows.Select(cols => cols.Select(c => new Cell(c)).ToArray()).ToArray();
    }

    public BingoGameResult Play(IEnumerable<int> numbersToDraw)
    {
        var drawnNumbers = numbersToDraw.TakeWhile(number =>
        {
            var numberShouldBeDrawn = !HasWon();
            if (numberShouldBeDrawn)
                MarkDrawnNumber(number);
            return numberShouldBeDrawn;
        }).ToArray();

        return new BingoGameResult(drawnNumbers, this);
    }

    void MarkDrawnNumber(int drawnNumber)
    {
        var allCells = Rows.SelectMany(c => c);
        foreach (var cell in allCells.Where(c => c.Number == drawnNumber))
        {
            cell.Mark();
        }
    }

    bool HasWon()
    {
        var anyRowIsComplete = Rows.Any(r => r.All(cell => cell.IsMarked));
        var anyColumnIsComplete = Columns.Any(c => c.All(cell => cell.IsMarked));
        return anyRowIsComplete || anyColumnIsComplete;
    }

    public int GetSumOfUnmarkedNumber()
    {
        return Rows.SelectMany(c => c).Where(c => !c.IsMarked).Sum(c => c.Number);
    }

    Cell[][] Rows { get; }

    Cell[][] Columns
    {
        get
        {
            var length = Rows.First().Length;
            return Enumerable.Range(0, length).Select(index =>
            {
                return Rows.Select(r => r[index]).ToArray();
            }).ToArray();
        }
    }

    class Cell
    {
        public Cell(int number)
        {
            Number = number;
            IsMarked = false;
        }

        public int Number { get; }
        public bool IsMarked { get; private set; }

        public void Mark()
        {
            IsMarked = true;
        }
    }
}

public class BingoGameResult
{
    readonly int[] _drawnNumbers;
    readonly BingoGame _bingoGame;

    public BingoGameResult(int[] drawnNumbers, BingoGame bingoGame)
    {
        _drawnNumbers = drawnNumbers;
        _bingoGame = bingoGame;
    }

    public int CountOfNumbersDrawnUntilWin => _drawnNumbers.Length;

    public int GetScore()
    {
        return _bingoGame.GetSumOfUnmarkedNumber() * _drawnNumbers.Last();
    }
}