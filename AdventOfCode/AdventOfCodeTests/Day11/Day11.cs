using System.Linq;
using AdventOfCode.Day10;
using AdventOfCode.Day11;
using Xunit;

namespace AdventOfCodeTests.Day11;

public class Day11
{
    [Fact]
    public void Part1WorksForSampleData()
    {
        Assert.Equal(1656, Day11Puzzle.GetNumberOfFlashes(SampleData, 100));
    }

    [Fact]
    public void Part1WorksForRealData()
    {
        Assert.Equal(1632, Day11Puzzle.GetNumberOfFlashes(RealData, 100));
    }
    
    [Fact]
    public void Part2WorksForSampleData()
    {
        Assert.Equal(195, Day11Puzzle.GetFirstStepWhereOctopusFlashesAreSynchronised(SampleData));
    }

    [Fact]
    public void Part2WorksForRealData()
    {
        Assert.Equal(303, Day11Puzzle.GetFirstStepWhereOctopusFlashesAreSynchronised(RealData));
    }
    
    static OctopusGrid RealData => CreateOctopusGrid(FileHelper.ReadFromFile("Day11", "RealEnergyLevels.txt"));

    static OctopusGrid SampleData => CreateOctopusGrid(FileHelper.ReadFromFile("Day11", "SampleEnergyLevels.txt"));

    static OctopusGrid CreateOctopusGrid(string input)
    {
        var energyLevels = input.Split('\n')
            .Select(row => 
                row.Select(energyLevelString => int.Parse(energyLevelString.ToString()))
                    .ToArray())
            .ToArray();
        return OctopusGrid.Create(energyLevels);
    }
}