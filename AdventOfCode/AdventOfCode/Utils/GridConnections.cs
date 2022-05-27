namespace AdventOfCode.Utils;

public static class GridConnections
{
    public static IEnumerable<ConnectedPair<T>> GetAdjacentLocations<T>(IEnumerable<IEnumerable<T>> grid)
    {
        var gridArray = grid.Select(g => g.ToArray()).ToArray();
        return GetVerticallyConnectedPairs(gridArray).Concat(GetHorizontallyConnectedPairs(gridArray));
    }
    
    static IEnumerable<ConnectedPair<T>> GetVerticallyConnectedPairs<T>(T[][] grid)
    {
        return grid.Zip(grid.Skip(1), (row1, row2) => row1.Zip(row2, (top, bottom) => (top, bottom)))
            .Flatten()
            .Select(verticalPair => new ConnectedPair<T>(verticalPair.top, verticalPair.bottom));
    }
    
    static IEnumerable<ConnectedPair<T>> GetHorizontallyConnectedPairs<T>(T[][] grid)
    {
        return grid.SelectMany(gridRow =>
        {
            return gridRow.Zip(gridRow.Skip(1), (left, right) => (left, right))
                .Select(horizontalPair => new ConnectedPair<T>(horizontalPair.left, horizontalPair.right));
        });
    }
}

public record ConnectedPair<T>(T First, T Second);