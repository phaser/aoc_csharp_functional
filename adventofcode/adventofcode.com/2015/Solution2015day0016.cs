
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0016
{
    public record Feature(string Name, int Value);

    public record Sue(int Id, List<Feature> Features);

    public static int SolvePart1(string input)
        => ParseInput(input)
            .Map(list => list.Select(e => new
                {
                    Sue = e, Score =
                        (e.Features.Any(f => f is { Name: "children", Value: 3 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "cats", Value: 7 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "samoyeds", Value: 2 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "pomeranians", Value: 3 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "akitas", Value: 0 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "vizslas", Value: 0 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "goldfish", Value: 5 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "trees", Value: 3 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "cars", Value: 2 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "perfumes", Value: 1 }) ? 1 : 0)
                })
                .MaxBy(s => s.Score)!.Sue.Id);

    public static int SolvePart2(string input)
        => ParseInput(input)
            .Map(list => list.Select(e => new
                {
                    Sue = e, Score =
                        (e.Features.Any(f => f is { Name: "children", Value: 3 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "cats", Value: > 7 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "samoyeds", Value: 2 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "pomeranians", Value: < 3 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "akitas", Value: 0 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "vizslas", Value: 0 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "goldfish", Value: < 5 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "trees", Value: > 3 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "cars", Value: 2 }) ? 1 : 0) +
                        (e.Features.Any(f => f is { Name: "perfumes", Value: 1 }) ? 1 : 0)
                })
                .MaxBy(s => s.Score)!.Sue.Id);

    private static List<Sue> ParseInput(string input)
        => input.Split('\n').Select(l => l.Trim())
            .Select(l => Regex.Match(l, @"Sue (?<id>[0-9]+):(( ?(?<feature>[a-z]+): (?<feature_value>[0-9]+),?)\2*)+")
                .Map(match => FunctionalExtensions.Map(match.Groups["feature"].Captures.Zip(match.Groups["feature_value"].Captures,
                        (a, b) => new Feature(a.Value, b.Value.ToInt())).ToList(), features => new Sue(match.Groups["id"].Value.ToInt(), features))))
            .ToList();
}
