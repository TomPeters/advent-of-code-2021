namespace AdventOfCode.Day1;

public static class Day1Puzzle
{
    // Part 1
    public static int GetNumberOfMeasurementsLargerThatPreviousMeasurement(int[] measurements)
    {
        return measurements.Skip(1)
            .Zip(measurements)
            .Count(m => m.First > m.Second);
    }

    // Part 2
    public static int GetNumberOfThreeMeasurementSlidingWindowsLargerThanPreviousWindow(int[] measurements)
    {
        var threeMeasurementWindows = GetThreeMeasurementWindowSums(measurements).ToArray();
        return GetNumberOfMeasurementsLargerThatPreviousMeasurement(threeMeasurementWindows);
    }

    static IEnumerable<int> GetThreeMeasurementWindowSums(int[] measurements)
    {
        return measurements.Skip(2)
            .Zip(measurements.Skip(1), measurements)
            .Select(m => new [] { m.First, m.Second, m.Third}.Sum());
    }
}
