using System.Linq;
using System.Reflection;
using AdventOfCode.Day1;
using Xunit;

namespace AdventOfCodeTests.Day1;

public class Day1
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(7, Day1Puzzle.GetNumberOfMeasurementsLargerThatPreviousMeasurement(SampleMeasurements));
    }

    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(5, Day1Puzzle.GetNumberOfThreeMeasurementSlidingWindowsLargerThanPreviousWindow(SampleMeasurements));
    }

    [Fact]
    public void SolvePart1()
    {
        var data = FileHelper.ReadFromFile("Day1", "Measurements.txt");
        var measurements = data.Split("\n").Select(int.Parse).ToArray();

        Assert.Equal(1400, Day1Puzzle.GetNumberOfMeasurementsLargerThatPreviousMeasurement(measurements));
    }

    [Fact]
    public void SolvePart2()
    {
        var data = FileHelper.ReadFromFile("Day1", "Measurements.txt");
        var measurements = data.Split("\n").Select(int.Parse).ToArray();

        Assert.Equal(1429, Day1Puzzle.GetNumberOfThreeMeasurementSlidingWindowsLargerThanPreviousWindow(measurements));
    }

    static int[] SampleMeasurements
    {
        get
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
            return sampleMeasurements;
        }
    }
}