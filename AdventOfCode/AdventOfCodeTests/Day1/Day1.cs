using System.Linq;
using System.Reflection;
using AdventOfCode.Day1;
using Xunit;

namespace AdventOfCodeTests.Day1;

public class Day1
{
    [Fact]
    public void WorksForSampleData()
    {
        var sampleMeasurements = new int[]
        {
            199,
            200,
            208,
            210,
            200,
            207,
            240,
            269,
            260,
            263
        };
        Assert.Equal(7, Day1Puzzle.GetNumberOfMeasurementsLargerThatPreviousMeasurement(sampleMeasurements));
    }

    [Fact]
    public void SolvePuzzle()
    {
        var data = FileHelper.ReadFromFile("Day1", "Measurements.txt");
        var measurements = data.Split("\n").Select(int.Parse).ToArray();

        Assert.Equal(1400, Day1Puzzle.GetNumberOfMeasurementsLargerThatPreviousMeasurement(measurements));
    }
}