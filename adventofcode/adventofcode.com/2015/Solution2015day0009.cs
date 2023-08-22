
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
            .And(SolveInternal);

    private static int SolveInternal(IList<Route> routes)
    {
        var link = new List<Route>();
        link.Add(routes.First());
        routes.RemoveAt(0);
        while (routes.Count > 0)
        {
            var newRoute = routes.First(r => r.City1 == link[0].City1
                                       || r.City1 == link[0].City2
                                       || r.City2 == link[0].City1
                                       || r.City2 == link[0].City2);
            link.Add(newRoute);
            routes.Remove(newRoute);
        }
        return link.Select(r => r.Distance)
            .Aggregate((a, b) => a + b);
    }

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
