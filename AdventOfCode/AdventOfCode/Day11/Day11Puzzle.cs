using AdventOfCode.Utils;

namespace AdventOfCode.Day11;

public static class Day11Puzzle
{
    public static int GetNumberOfFlashes(OctopusGrid octopusGrid, int numberOfSteps)
    {
        Enumerable.Range(0, numberOfSteps).ForEach(_ => octopusGrid.Step());
        return octopusGrid.NumberOfFlashes;
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

    public void Step()
    {
        _octopuses.ForEach(o => o.IncreaseEnergyLevel());
        _octopuses.ForEach(o => o.FlashIfEnergyLevelHighEnough());
        _octopuses.ForEach(o => o.ResetEnergyLevelIfFlashed());
    }

    public int NumberOfFlashes => _octopuses.Sum(o => o.NumberOfFlashes);

    record OctopusWithCoordinates(Octopus Octopus, int RowIndex, int ColumnIndex);
}

public class Octopus
{
    int _energyLevel;
    int _numberOfFlashes = 0;
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
        var originalEnergyLevel = _energyLevel;
        _energyLevel++;
        if (originalEnergyLevel <= EnergyLevelRequiredForFlash)
        {
            FlashIfEnergyLevelHighEnough();
        }
    }

    public void FlashIfEnergyLevelHighEnough()
    {
        if (_energyLevel > EnergyLevelRequiredForFlash)
        {
            Flash();
        }
    }

    void Flash()
    {
        _numberOfFlashes++;
        _adjacentOctopuses.ForEach(o => o.OnAdjacentOctopusFlashed());
    }

    public void ResetEnergyLevelIfFlashed()
    {
        if (_energyLevel > EnergyLevelRequiredForFlash)
        {
            _energyLevel = 0;
        }
    }

    public void ConnectAdjacentOctopus(Octopus octopus)
    {
        _adjacentOctopuses.Add(octopus);
        octopus._adjacentOctopuses.Add(this);
    }

    public int NumberOfFlashes => _numberOfFlashes;
}