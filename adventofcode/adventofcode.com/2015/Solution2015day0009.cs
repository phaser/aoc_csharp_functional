
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0009
{
    public record Route(string City1, string City2, int Distance);

    public static int SolvePart1(string input)
        => ParseInput(input)
            .And(routes => routes.Modify(r =>
            {
                r.Sort((a, b) => a.Distance < b.Distance ? -1 : a.Distance == b.Distance ? 0 : 1);
            }))
            .And(routes => SolveInternal(routes).Min());

    public static int SolvePart2(string input)
        => ParseInput(input)
            .And(routes => routes.Modify(r =>
            {
                r.Sort((a, b) => a.Distance < b.Distance ? -1 : a.Distance == b.Distance ? 0 : 1);
            }))
            .And(routes => SolveInternal(routes).Max());

    private static IEnumerable<int> SolveInternal(IList<Route> routes)
        => routes
            .SelectMany(route => new List<string>(2) { route.City1, route.City2 }.AsEnumerable())
            .Distinct()
            .ToArray()
            .Permutations()
            .Select(p => p.Zip(p.Skip(1), (a, b) => routes.First(
                    r => (r.City1 == a && r.City2 == b)
                         || (r.City1 == b && r.City2 == a)).Distance)
                .Aggregate((a, b) => a + b));
    
    private static List<Route> ParseInput(string input)
        => input
            .Split('\n')
            .Select(line => line.Trim())
            .Select(line =>
                InputFormatRegex().Match(line)
                    .And(match => match.Success
                        ? new Route(match.Groups["city1"].Value.Trim(), match.Groups["city2"].Value.Trim(),
                            int.Parse(match.Groups["distance"].Value))
                        : throw new ArgumentException("Invalid input")))
            .ToList();

    [GeneratedRegex("(?<city1>[a-zA-Z]+ )to (?<city2>[a-zA-Z]+) = (?<distance>[0-9]+)")]
    private static partial Regex InputFormatRegex();
}
