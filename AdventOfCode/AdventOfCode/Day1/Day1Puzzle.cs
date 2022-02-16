namespace AdventOfCode.Day1;

public static class Day1Puzzle
{
    public static int GetNumberOfMeasurementsLargerThatPreviousMeasurement(int[] measurements)
    {
        return measurements.Skip(1).Zip(measurements).Count(m => m.First > m.Second);
    }
}