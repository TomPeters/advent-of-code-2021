namespace AdventOfCode.Day6;

public static class Day6Puzzle
{
    public static long NumberOfLanternFishAfterDays(IEnumerable<int> initialLanternFishInternalTimers, int numberOfDays)
    {
        var initialLanternFish = initialLanternFishInternalTimers.Select(t => new SynchronisedLanternFishGroup(1, t));
        var groupedInitialLanternFish = SynchronisedLanternFishGroup.GroupFishWithSameTimers(initialLanternFish);
        var finalLanternFishGroups = Enumerable.Range(0, numberOfDays).Aggregate(groupedInitialLanternFish, (lanternFishGroups, _) =>
        {
            var lanternFishGroupsOnNextDay = lanternFishGroups.SelectMany(g => g.LanternFishGroupsOnNextDay());
            return SynchronisedLanternFishGroup.GroupFishWithSameTimers(lanternFishGroupsOnNextDay);
        });
        return finalLanternFishGroups.Sum(g => g.NumberOfFishInGroup);
    }
}

class SynchronisedLanternFishGroup
{
    public long NumberOfFishInGroup { get; }
    readonly int _internalTimer;

    public SynchronisedLanternFishGroup(long numberOfFishInGroup, int internalTimer)
    {
        NumberOfFishInGroup = numberOfFishInGroup;
        _internalTimer = internalTimer;
    }

    public IEnumerable<SynchronisedLanternFishGroup> LanternFishGroupsOnNextDay()
    {
        if (_internalTimer == 0)
        {
            return new[]
            {
                new SynchronisedLanternFishGroup(NumberOfFishInGroup, 6),
                new SynchronisedLanternFishGroup(NumberOfFishInGroup, 8)
            };
        }

        return new[] { new SynchronisedLanternFishGroup(NumberOfFishInGroup, _internalTimer - 1) };
    }

    public static IEnumerable<SynchronisedLanternFishGroup> GroupFishWithSameTimers(
        IEnumerable<SynchronisedLanternFishGroup> fishGroups)
    {
        return fishGroups.GroupBy(g => g._internalTimer).Select(groupOfGroups =>
            new SynchronisedLanternFishGroup(groupOfGroups.Sum(group => group.NumberOfFishInGroup), groupOfGroups.Key));
    }
}