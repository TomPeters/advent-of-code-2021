namespace AdventOfCode.Day7;

public static class Day7Puzzle
{
    public static int GetMinFuelToAlignCrabs(int[] initialCrabPositions, IFuelCostCalculator fuelCostCalculator)
    {
        return GetPossibleAlignmentPositions(initialCrabPositions)
            .Select(p => GetFuelCostOfAlignment(p, initialCrabPositions, fuelCostCalculator))
            .Min();
    }

    static int GetFuelCostOfAlignment(int positionToAlignTo, IEnumerable<int> initialCrabPositions, IFuelCostCalculator fuelCostCalculator)
    {
        return initialCrabPositions.Sum(initialCrabPosition =>
        {
            var distance = Math.Abs(initialCrabPosition - positionToAlignTo);
            return fuelCostCalculator.GetCostOfFuel(distance);
        });
    }

    static IEnumerable<int> GetPossibleAlignmentPositions(int[] positions)
    {
        var maxPosition = positions.Max();
        var minPosition = positions.Min();
        var range = maxPosition - minPosition;
        return Enumerable.Range(minPosition, range);
    }
}

public interface IFuelCostCalculator
{
    int GetCostOfFuel(int distanceToMove);
}

public class ConstantFuelCostCalculator : IFuelCostCalculator
{
    public int GetCostOfFuel(int distanceToMove) => distanceToMove;
}

public class IncrementingFuelCostCalculator : IFuelCostCalculator
{
    public int GetCostOfFuel(int distanceToMove) => distanceToMove * (distanceToMove + 1) / 2;
}
