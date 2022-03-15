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
}