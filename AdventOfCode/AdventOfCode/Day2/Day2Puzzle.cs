namespace AdventOfCode.Day2;

public static class Day2Puzzle
{
    // Part 1
    public static int CalculateProductOfFinalPositionAndDepth(IEnumerable<string> movementCommands)
    {
        var finalPosition = movementCommands.Select(GetPositionAdjustments)
            .Aggregate(Position.Origin, (position, adjustment) => adjustment(position));
        return finalPosition.Depth * finalPosition.HorizontalPosition;
    }

    static Func<Position, Position> GetPositionAdjustments(string command)
    {
        var commandParts = command.Split(" ");
        var commandType = commandParts[0];
        var offset = int.Parse(commandParts[1]);
        return commandType switch
        {
            "forward" => position => position.OffsetHorizontally(offset),
            "down" => position => position.OffsetDepth(offset),
            "up" => position => position.OffsetDepth(-offset),
            _ => throw new Exception($"Command type not recognised {commandType}")
        };
    }

    // Part 2
    public static int CalculateProductOfFinalPositionAndDepthWhenAccountingForAim(IEnumerable<string> movementCommands)
    {
        var finalPositionAndAim = movementCommands.Select(GetPositionAndAimAdjustments)
            .Aggregate(PositionAndAim.StartingPositionAndAim, (positionAndAim, adjustment) => adjustment(positionAndAim));
        return finalPositionAndAim.Position.Depth * finalPositionAndAim.Position.HorizontalPosition;
    }

    static Func<PositionAndAim, PositionAndAim> GetPositionAndAimAdjustments(string command)
    {
        var commandParts = command.Split(" ");
        var commandType = commandParts[0];
        var adjustment = int.Parse(commandParts[1]);
        return commandType switch
        {
            "forward" => position => position.OffsetHorizontally(adjustment),
            "down" => position => position.AdjustAim(adjustment),
            "up" => position => position.AdjustAim(-adjustment),
            _ => throw new Exception($"Command type not recognised {commandType}")
        };
    }
}

class Position
{
    public static readonly Position Origin = new(0, 0);

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

class PositionAndAim
{
    public static readonly PositionAndAim StartingPositionAndAim = new(Position.Origin, 0);

    PositionAndAim(Position position, int aim)
    {
        Position = position;
        Aim = aim;
    }

    public Position Position { get; }
    public int Aim { get; }

    public PositionAndAim OffsetHorizontally(int horizontalOffset)
    {
        return new PositionAndAim(Position.OffsetHorizontally(horizontalOffset).OffsetDepth(Aim * horizontalOffset),
            Aim);
    }

    public PositionAndAim AdjustAim(int aimAdjustment)
    {
        return new PositionAndAim(Position, Aim + aimAdjustment);
    }
}