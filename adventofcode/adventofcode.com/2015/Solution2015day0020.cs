
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace adventofcode.adventofcode.com._2015;

public class CompareELfs : IComparer<(int ElfId, int House)>
{
    public int Compare((int ElfId, int House) x, (int ElfId, int House) y)
    {
        return Comparer<int>.Default.Compare(x.Item2, y.Item2);
    }
}

public static class Solution2015day0020
{
    // This takes 25 seconds
    public static int SolveB(int limit, int numberOfDeliveredPresentsPerHouse, int numberOfStops)
        => Enumerable.Range(1, limit / 10)
            .SelectMany(elfIdx => 
                Enumerable
                    .Range(1, Math.Min((limit / 10) / elfIdx, numberOfStops))
                    .Select(idx => (
                        HouseIndex: idx * elfIdx,
                        Presents: elfIdx * numberOfDeliveredPresentsPerHouse)))
            .GroupBy(h => h.HouseIndex)
            .Select(group => (HouseIndex: group.Key, Sum: group.Select(g => g.Presents).Sum()))
            .First(house => house.Sum >= limit).HouseIndex;

    // this takes 2 seconds 
    // just part 1 - I just validated that I can make it faster and keep it functional
    public static int Solve(int limit, int numberOfDeliveredPresentsPerHouse, int numberOfStops)
        => CreateDictionaryOfHouses(limit)
            .Map(currentElvesHouses => GetFirstHouseOverTheLimit(limit, currentElvesHouses));

    private static int GetFirstHouseOverTheLimit(int limit, ReadOnlyDictionary<int, List<int>> currentElvesHouses)
        => Enumerable.Range(1, limit / 10)
            .First(house => (currentElvesHouses[house]
                    .Select(elf => 
                    { 
                        currentElvesHouses[house + elf].Add(elf); 
                        return elf; 
                    })
                    .Tap(_ => currentElvesHouses[2 * house].Add(house))
                    .Select(elf => elf * 10).Sum() + house * 10)
                .Tap(_ => currentElvesHouses[house].Clear()) >= limit);

    private static ReadOnlyDictionary<int, List<int>> CreateDictionaryOfHouses(int limit)
        => new(new Dictionary<int, List<int>>(Enumerable.Range(1, limit / 10).Select(house => new KeyValuePair<int, List<int>>(house, new List<int>()))));

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
