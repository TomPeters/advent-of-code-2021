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
        var threeMeasurementWindows = GetThreeMeasurementWindows(measurements).ToArray();
        return threeMeasurementWindows.Skip(1)
            .Zip(threeMeasurementWindows)
            .Count(m => m.First.Sum() > m.Second.Sum());
    }

    static IEnumerable<ThreeMeasurementWindow> GetThreeMeasurementWindows(int[] measurements)
    {
        return measurements.Skip(2)
            .Zip(measurements.Skip(1), measurements)
            .Select(m => new ThreeMeasurementWindow(new[] { m.First, m.Second, m.Third }));
    }
}

class ThreeMeasurementWindow
{
    readonly int[] _values;

    public ThreeMeasurementWindow(int[] values)
    {
        _values = values;
    }

    public int Sum()
    {
        return _values.Sum();
    }
}