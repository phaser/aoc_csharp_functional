
using System.Diagnostics.CodeAnalysis;

namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0020
{
    // This takes 25 seconds
    public static int Solve(int limit, int numberOfDeliveredPresentsPerHouse, int numberOfStops)
        => Enumerable.Range(1, limit / 10)
            .SelectMany(elfIdx => Enumerable.Range(1, Math.Min((limit / 10) / elfIdx, numberOfStops))
                .Select(idx => (HouseIndex: idx * elfIdx, Presents: elfIdx * numberOfDeliveredPresentsPerHouse)))
            .GroupBy(h => h.HouseIndex)
            .Select(group => (HouseIndex: group.Key, Sum: group.Select(g => g.Presents).Sum()))
            .First(house => house.Sum >= limit).HouseIndex;

    // This takes 273 ms
    public static int SolveIterative(int limit, int numberOfDeliveredPresentsPerHouse, int numberOfStops)
    {
        var houses = new int[limit / 10];
        for (var i = 1; i < houses.Length; i++)
        for (var j = i; j < houses.Length; houses[j] += i * numberOfDeliveredPresentsPerHouse, j += i)
            if (j / i >= numberOfStops)
                break;

        for (var i = 1; i < houses.Length; i++)
                if (houses[i] >= limit)
                    return i;
        return 0;
    }
}
