namespace AdventOfCode.Day7;

public static class Day7Puzzle
{
    public static int GetFuelToAlignToPosition(int[] positions, IFuelCostCalculator fuelCostCalculator)
    {
        var positionRange = GetPositionRange(positions);

        return Enumerable.Range(positionRange.MinPosition, positionRange.Range)
            .Select(p => GetCostOfAlignment(p, positions, fuelCostCalculator))
            .Min();
    }

    static int GetCostOfAlignment(int positionToAlignTo, int[] positions, IFuelCostCalculator fuelCostCalculator)
    {
        return positions.Sum(p =>
        {
            var distance = Math.Abs(p - positionToAlignTo);
            return fuelCostCalculator.GetCostOfFuel(distance);
        });
    }

    static PositionRange GetPositionRange(int[] positions)
    {
        var maxPosition = positions.Max();
        var minPosition = positions.Min();
        return new PositionRange(maxPosition, minPosition);
    }
}

public interface IFuelCostCalculator
{
    int GetCostOfFuel(int distanceToMove);
}

public class ConstantFuelCostCalculator : IFuelCostCalculator
{
    public int GetCostOfFuel(int distanceToMove)
    {
        return distanceToMove;
    }
}

public class IncrementingFuelCostCalculator : IFuelCostCalculator
{
    public int GetCostOfFuel(int distanceToMove)
    {
        if (distanceToMove % 2 == 0)
        {
            return GetCostOfFuel(distanceToMove + 1) - (distanceToMove + 1);
        }

        var oneValueHigher = distanceToMove + 1;
        return (oneValueHigher * oneValueHigher / 2) - (oneValueHigher / 2);
    }
}

class PositionRange
{
    public int MaxPosition { get; }
    public int MinPosition { get; }
    public int Range => MaxPosition - MinPosition;

    public PositionRange(int maxPosition, int minPosition)
    {
        this.MaxPosition = maxPosition;
        this.MinPosition = minPosition;
    }
}