using AdventOfCode.Utils;

namespace AdventOfCode.Day11;

public static class Day11Puzzle
{
    public static int GetNumberOfFlashes(OctopusGrid octopusGrid, int numberOfSteps)
    {
        Enumerable.Range(0, numberOfSteps).ForEach(_ => octopusGrid.Step());
        return octopusGrid.NumberOfFlashes;
    }

    public static int GetFirstStepWhereOctopusFlashesAreSynchronised(OctopusGrid octopusGrid)
    {
        return EnumerableExtensions.NaturalNumbers().First(_ => octopusGrid.Step().AllOctopusesWereSynchronised);
    }
}

public class OctopusGrid
{
    readonly Octopus[] _octopuses;

    OctopusGrid(IEnumerable<Octopus> octopuses)
    {
        _octopuses = octopuses.ToArray();
    }

    public static OctopusGrid Create(int[][] energyLevels)
    {
        var octopusGridRows = energyLevels.Select((row, rowIndex) =>
        {
            return row.Select((energyLevel, columnIndex) =>
                    new OctopusWithCoordinates(new Octopus(energyLevel), rowIndex, columnIndex)).ToArray();
        }).ToArray();

        ConnectAdjacentOctopuses(octopusGridRows);

        return new OctopusGrid(octopusGridRows.Flatten().Select(o => o.Octopus));
    }

    static void ConnectAdjacentOctopuses(OctopusWithCoordinates[][] octopusGridRows)
    {
        var maxRowIndex = octopusGridRows.Length - 1;
        var maxColumnIndex = octopusGridRows.First().Length - 1;

        octopusGridRows.Flatten().ForEach(octopusWithCoords =>
        {
            var rowIndexMin = Math.Max(octopusWithCoords.RowIndex - 1, 0);
            var rowIndexMax = Math.Min(octopusWithCoords.RowIndex + 1, maxRowIndex);
            var columnIndexMin = Math.Max(octopusWithCoords.ColumnIndex - 1, 0);
            var columnIndexMax = Math.Min(octopusWithCoords.ColumnIndex + 1, maxColumnIndex);

            var octopusEnumerable = EnumerableExtensions.BidirectionalRange(rowIndexMin, rowIndexMax).SelectMany(
                _ => EnumerableExtensions.BidirectionalRange(columnIndexMin, columnIndexMax),
                (rowIndex, columnIndex) => octopusGridRows[rowIndex][columnIndex]).ToList();
            var adjacentOctopuses = octopusEnumerable.Select(o => o.Octopus)
                .Except(new[] { octopusWithCoords.Octopus });

            adjacentOctopuses.ForEach(o => o.ConnectAdjacentOctopus(octopusWithCoords.Octopus));
        });
    }
    
    public StepResult Step()
    {
        var initialNumberOfFlashes = NumberOfFlashes;
        _octopuses.ForEach(o => o.IncreaseEnergyLevel());
        _octopuses.ForEach(o => o.FlashIfEnergyLevelHighEnough());
        _octopuses.ForEach(o => o.ResetForNextStep());
        var finalNumberOfFlashes = NumberOfFlashes;
        var numberOfFlashesDuringThisStep = finalNumberOfFlashes - initialNumberOfFlashes;
        var allOctopusesWereSynchronised = numberOfFlashesDuringThisStep == _octopuses.Length;
        return new StepResult(allOctopusesWereSynchronised);
    }
    
    public int NumberOfFlashes => _octopuses.Sum(o => o.NumberOfFlashes);

    record OctopusWithCoordinates(Octopus Octopus, int RowIndex, int ColumnIndex);
}

public record StepResult(bool AllOctopusesWereSynchronised);

public class Octopus
{
    int _energyLevel;
    int _numberOfFlashes;
    bool _hasBeenFlashedThisStep;
    readonly HashSet<Octopus> _adjacentOctopuses = new();
    
    const int EnergyLevelRequiredForFlash = 9;

    public Octopus(int energyLevel)
    {
        _energyLevel = energyLevel;
    }

    public void IncreaseEnergyLevel()
    {
        _energyLevel++;
    }

    void OnAdjacentOctopusFlashed()
    {
        _energyLevel++;
        FlashIfEnergyLevelHighEnough();
    }

    public void FlashIfEnergyLevelHighEnough()
    {
        if (_energyLevel > EnergyLevelRequiredForFlash && !_hasBeenFlashedThisStep)
        {
            Flash();
        }
    }

    void Flash()
    {
        _numberOfFlashes++;
        _hasBeenFlashedThisStep = true;
        _adjacentOctopuses.ForEach(o => o.OnAdjacentOctopusFlashed());
    }

    public void ResetForNextStep()
    {
        if (_hasBeenFlashedThisStep)
        {
            _energyLevel = 0;
            _hasBeenFlashedThisStep = false;
        }
    }

    public void ConnectAdjacentOctopus(Octopus octopus)
    {
        _adjacentOctopuses.Add(octopus);
        octopus._adjacentOctopuses.Add(this);
    }

    public int NumberOfFlashes => _numberOfFlashes;
}