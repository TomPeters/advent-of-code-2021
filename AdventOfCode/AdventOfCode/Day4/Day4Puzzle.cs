namespace AdventOfCode.Day4;

public class Day4Puzzle
{
    public static int GetScoreOfWinningBoard(IBingoPuzzleInput input)
    {
        return input.Boards.Select(b => PlayBingoToCompletion(b, input.DrawnNumbers)).OrderBy(g => g.CountOfNumbersDrawnUntilWin).First().GetScore();
    }

    static BingoGame PlayBingoToCompletion(BingoBoard bingoBoard, IEnumerable<int> numbersToDraw)
    {
        var bingoGame = bingoBoard.CreateNewGame();

        foreach (var drawnNumber in numbersToDraw)
        {
            bingoGame.MarkDrawnNumber(drawnNumber);
        }
        return bingoGame;
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

    public BingoGame CreateNewGame()
    {
        return new BingoGame(_rows);
    }

}

public class BingoGame
{
    int _lastNumberDrawn;
    public BingoGame(IEnumerable<IEnumerable<int>> rows)
    {
        Rows = rows.Select(cols => cols.Select(c => new Cell(c)).ToArray()).ToArray();
        CountOfNumbersDrawnUntilWin = 0;
    }

    public int GetScore()
    {
        return Rows.SelectMany(c => c).Where(c => !c.IsMarked).Sum(c => c.Number) * _lastNumberDrawn;
    }

    public void MarkDrawnNumber(int drawnNumber)
    {
        if (HasWon()) return;

        _lastNumberDrawn = drawnNumber;

        var allCells = Rows.SelectMany(c => c);
        foreach (var cell in allCells.Where(c => c.Number == drawnNumber))
        {
            cell.Mark();
        }

        CountOfNumbersDrawnUntilWin++;
    }

    public int CountOfNumbersDrawnUntilWin { get; private set; }

    Cell[][] Rows { get; set; }

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

    public bool HasWon()
    {
        var anyRowIsComplete = Rows.Any(r => r.All(cell => cell.IsMarked));
        var anyColumnIsComplete = Columns.Any(c => c.All(cell => cell.IsMarked));
        return anyRowIsComplete || anyColumnIsComplete;
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