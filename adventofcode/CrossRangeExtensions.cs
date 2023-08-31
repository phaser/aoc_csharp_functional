namespace adventofcode;

public static class CrossRangeExtensions
{
    public static IEnumerable<(int x, int y)> CrossRange(this (int Width, int Height) dims)
        => Enumerable.Range(0, dims.Width * dims.Height)
            .Select(v => (v % dims.Width, v / dims.Width));
}