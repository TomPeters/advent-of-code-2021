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
        var finalSubmarine = movementCommandStrings.Select(GetSubmarineCommands)
            .Aggregate(submarine, (sub, command) => command(sub));
        return finalSubmarine.Position.Depth * finalSubmarine.Position.HorizontalPosition;
    }

    static Func<ISubmarine, ISubmarine> GetSubmarineCommands(string command)
    {
        var commandParts = command.Split(" ");
        var commandType = commandParts[0];
        var units = int.Parse(commandParts[1]);
        return commandType switch
        {
            "forward" => submarine => submarine.Forward(units),
            "down" => submarine => submarine.Down(units),
            "up" => submarine => submarine.Up(units),
            _ => throw new Exception($"Command type not recognised {commandType}")
        };
    }

}

interface ISubmarine
{
    Position Position { get; }
    ISubmarine Forward(int units);
    ISubmarine Down(int units);
    ISubmarine Up(int units);
}

class SubmarineWithAim : ISubmarine
{
    readonly int _aim;
    public Position Position { get; }

    public SubmarineWithAim(Position position) : this(position, 0)
    {
    }

    SubmarineWithAim(Position position, int aim)
    {
        _aim = aim;
        Position = position;
    }

    public ISubmarine Forward(int units)
    {
        return new SubmarineWithAim(Position.OffsetHorizontally(units).OffsetDepth(_aim * units), _aim);
    }

    public ISubmarine Down(int units)
    {
        return new SubmarineWithAim(Position, _aim + units);
    }

    public ISubmarine Up(int units)
    {
        return new SubmarineWithAim(Position, _aim - units);
    }
}

class Submarine : ISubmarine
{
    public Position Position { get; }

    public Submarine(Position position)
    {
        Position = position;
    }

    public ISubmarine Forward(int units) => new Submarine(Position.OffsetHorizontally(units));
    public ISubmarine Down(int units) => new Submarine(Position.OffsetDepth(units));
    public ISubmarine Up(int units) => new Submarine(Position.OffsetDepth(-units));
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