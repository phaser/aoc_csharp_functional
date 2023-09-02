
using System.Diagnostics.CodeAnalysis;

namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0020
{
    public static int SolvePart1(int limit)
        => Enumerable.Range(1, limit / 10)
            .SelectMany(elfIdx => Enumerable.Range(1, (limit / 10 + 1) / elfIdx)
                .Select(idx => (HouseIndex: idx * elfIdx, Presents: elfIdx * 10)))
            .AsParallel()
            .GroupBy(h => h.HouseIndex)
            .Select(group => (HouseIndex: group.Key, Sum: group.Select(g => g.Presents).Sum()))
            .First(house => house.Sum >= limit).HouseIndex;
}
