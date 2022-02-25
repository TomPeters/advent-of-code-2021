namespace AdventOfCode.Day2;

public static class Day2Puzzle
{
    // Part 1
    public static int Part1CalculateProductOfFinalPositionAndDepth(IEnumerable<string> movementCommandStrings)
    {
        return CalculateProductOfFinalPositionAndDepth(new Submarine(Position.Origin), movementCommandStrings);
    }

    // Part 2
    public static int Part2CalculateProductOfFinalPositionAndDepth(IEnumerable<string> movementCommandStrings)
    {
        return CalculateProductOfFinalPositionAndDepth(new SubmarineWithAim(Position.Origin), movementCommandStrings);
    }

    static int CalculateProductOfFinalPositionAndDepth(ISubmarine submarine, IEnumerable<string> movementCommandStrings)
    {
        foreach (var command in movementCommandStrings.Select(GetSubmarineCommands))
        {
            command.Move(submarine);
        }
        return submarine.Position.Depth * submarine.Position.HorizontalPosition;
    }

    static IMovementCommand GetSubmarineCommands(string command)
    {
        var commandParts = command.Split(" ");
        var commandType = commandParts[0];
        var units = int.Parse(commandParts[1]);
        return commandType switch
        {
            "forward" => new MoveForwardCommand(units),
            "down" => new MoveDownCommand(units),
            "up" => new MoveUpCommand(units),
            _ => throw new Exception($"Command type not recognised {commandType}")
        };
    }
}

interface IMovementCommand
{
    void Move(ISubmarine submarine);
}

class MoveForwardCommand : IMovementCommand
{
    readonly int _units;

    public MoveForwardCommand(int units)
    {
        _units = units;
    }

    public void Move(ISubmarine submarine)
    {
        submarine.Forward(_units);
    }
}

class MoveDownCommand : IMovementCommand
{
    readonly int _units;

    public MoveDownCommand(int units)
    {
        _units = units;
    }

    public void Move(ISubmarine submarine)
    {
        submarine.Down(_units);
    }
}

class MoveUpCommand : IMovementCommand
{
    readonly int _units;

    public MoveUpCommand(int units)
    {
        _units = units;
    }

    public void Move(ISubmarine submarine)
    {
        submarine.Up(_units);
    }
}

interface ISubmarine
{
    Position Position { get; }
    void Forward(int units);
    void Down(int units);
    void Up(int units);
}

class SubmarineWithAim : ISubmarine
{
    int _aim;
    public Position Position { get; private set; }

    public SubmarineWithAim(Position position) : this(position, 0)
    {
    }

    SubmarineWithAim(Position position, int aim)
    {
        _aim = aim;
        Position = position;
    }

    public void Forward(int units)
    {
        Position = Position.OffsetHorizontally(units).OffsetDepth(_aim * units);
    }

    public void Down(int units)
    {
        _aim += units;
    }

    public void Up(int units)
    {
        _aim -= units;
    }
}

class Submarine : ISubmarine
{
    public Position Position { get; private set; }

    public Submarine(Position position)
    {
        Position = position;
    }

    public void Forward(int units) => Position = Position.OffsetHorizontally(units);
    public void Down(int units) => Position = Position.OffsetDepth(units);
    public void Up(int units) => Position = Position.OffsetDepth(-units);
}

class Position
{
    public static readonly Position Origin = new (0, 0);

    Position(int horizontalPosition, int depth)
    {
        HorizontalPosition = horizontalPosition;
        Depth = depth;
    }

    public int HorizontalPosition { get; }
    public int Depth { get; }

    public Position OffsetHorizontally(int horizontalOffset)
    {
        return new Position(HorizontalPosition + horizontalOffset, Depth);
    }

    public Position OffsetDepth(int depthOffset)
    {
        return new Position(HorizontalPosition, Depth + depthOffset);
    }
}