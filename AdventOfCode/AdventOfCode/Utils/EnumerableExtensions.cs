namespace AdventOfCode.Utils;

public static class EnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> input, int batchSize)
    {
        if (batchSize <= 0) throw new ArgumentException("Batch size must be a positive number", nameof(batchSize));

        var currentBatch = new List<T>();
        foreach (var item in input)
        {
            currentBatch.Add(item);
            if (currentBatch.Count != batchSize) continue;
            yield return currentBatch;
            currentBatch = new List<T>();
        }

        if (currentBatch.Any())
        {
            yield return currentBatch;
        }
    }

    public static IEnumerable<int> BidirectionalRange(int start, int end)
    {
        if (start == end)
        {
            return new [] { start };
        }

        return end > start
            ? Enumerable.Range(start, end - start + 1)
            : Enumerable.Range(end, start - end + 1).Reverse();
    }

    public static int Pow(this int bas, int exp)
    {
        return Enumerable
            .Repeat(bas, exp)
            .Aggregate(1, (a, b) => a * b);
    }
}