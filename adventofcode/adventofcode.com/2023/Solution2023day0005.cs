
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public static partial class Solution2023day0005
{
    private static string[] _maps = new string[]
    {
        "seed-to-soil map:",
        "soil-to-fertilizer map:",
        "fertilizer-to-water map:",
        "water-to-light map:",
        "light-to-temperature map:",
        "temperature-to-humidity map:",
        "humidity-to-location map:",
        "TERMINATOR"
    };

    public record MapRange(long SourceStart, long DestinationStart, long Length);
    
    private record Map(IList<Range> Ranges);

    public static long SolvePart1(string input)
        => ParseSeedsPart1(input)
            .Map(seeds => ParseMaps(input)
                .Map(maps => SolvePart1(maps, seeds)));

    public static long SolvePart2(string input)
        => ParseSeedsPart1(input)
            .Map(seeds => ParseMaps(input)
                .Map(maps => CreatePairs(seeds)
                    .AsParallel()
                    .Select(seedsRange => SolvePart1(maps, EnumerableExtensions.CreateRange(seedsRange.Item1, seedsRange.Item2).ToList()))))
            .Min();

    private static IEnumerable<Tuple<long, long>> CreatePairs(List<long> seeds)
        => seeds
            .Where((s, idx) => idx % 2 == 0)
            .Zip(seeds.Where((_, idx) => idx % 2 != 0), Tuple.Create);

    private static long SolvePart1(List<List<MapRange>> maps, List<long> seeds)
        => seeds.Map(MapSeeds(maps))
            .Aggregate((a, b) => b)
            .Map(finalSeeds => finalSeeds.Min());

    private static Func<List<long>, IEnumerable<List<long>>> MapSeeds(List<List<MapRange>> maps)
        => seeds => maps.Select(map => seeds = seeds
            .Select(seed => map.FirstOrDefault(m => m.IsInSourceRange(seed))
                .Map(containingMap => containingMap == null
                    ? seed
                    : (seed - containingMap.SourceStart + containingMap.DestinationStart)))
            .ToList());

    private static List<long> ParseSeedsPart1(string input)
        => NumberRegex().Matches(input.Split("\n")[0])
            .Select(match => long.Parse(match.Value))
            .ToList();

    private static List<List<MapRange>> ParseMaps(string input)
        => _maps.Zip(_maps.Skip(1), (a, b) => input
                .Split("\n")
                .SkipWhile(line => !line.Contains(a))
                .TakeWhile(line => !line.Contains(b))
                .Where(line => !string.IsNullOrEmpty(line.Trim()))
                .Skip(1)
                .Select(line => NumberRegex().Matches(line)
                    .Map(matches => new MapRange(
                        long.Parse(matches[1].Value), 
                        long.Parse(matches[0].Value),
                        long.Parse(matches[2].Value))))
                .ToList())
            .ToList();

    public static bool IsInSourceRange(this MapRange range, long number)
        => number >= range.SourceStart && number < range.SourceStart + range.Length;
        
    [GeneratedRegex("[0-9]+")]
    private static partial Regex NumberRegex();
}
