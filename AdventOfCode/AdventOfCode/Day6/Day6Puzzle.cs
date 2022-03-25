namespace AdventOfCode.Day6;

public static class Day6Puzzle
{
    public static int NumberOfLanternFishAfterDays(IEnumerable<int> initialLanternFishInternalTimers, int numberOfDays)
    {
        var initialLanternFish = initialLanternFishInternalTimers.Select(t => new LanternFish(t));
        var finalLanternFish = Enumerable.Range(0, numberOfDays).Aggregate(initialLanternFish, (lanternFish, _) =>
        {
            return lanternFish.SelectMany(l => l.LanternFishOnNextDay());
        });
        return finalLanternFish.Count();
    }
}

class LanternFish
{
    readonly int _internalTimer;

    public LanternFish(int internalTimer)
    {
        _internalTimer = internalTimer;
    }

    public IEnumerable<LanternFish> LanternFishOnNextDay()
    {
        if (_internalTimer == 0)
        {
            return new[]
            {
                new LanternFish(6),
                new LanternFish(8)
            };
        }

        return new[] { new LanternFish(_internalTimer - 1) };
    }
}