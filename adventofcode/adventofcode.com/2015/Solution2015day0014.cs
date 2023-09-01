
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0014
{
    public record RainDeer(string Name, bool IsResting, int UntilStateChange, int MaxRun, int MaxRest, int DistPerSecond, int AccumulatedDistance, int WinningPoints);

    private record Race(List<RainDeer> Raindeers);

    // part 1 was done with pen & paper
    
    public static int SolvePart2(string input, int duration)
        => input.Split('\n').Select(s => s.Trim())
            .Select(ParseInput)
            .And(raindeers => new Race(raindeers.ToList()))
            .And(race => EvalState(race, duration))
            .And(l => l.Raindeers.MaxBy(ra => ra.WinningPoints)!.WinningPoints);

    private static RainDeer ParseInput(string l)
        => ParseInputRegex().Match(l)
            .And(match => new RainDeer(
                Name: match.Groups["name"].Value,
                IsResting: false, 
                UntilStateChange: int.Parse(match.Groups["speedburst"].Value),
                MaxRun: int.Parse(match.Groups["speedburst"].Value),
                MaxRest: int.Parse(match.Groups["rest"].Value),
                DistPerSecond: int.Parse(match.Groups["speed"].Value),
                AccumulatedDistance: 0, 
                WinningPoints: 0));

    private static Race EvalState(Race race, int duration)
        => Enumerable.Range(0, duration)
            .Select(_ => new Race(Raindeers: race.Raindeers
                    .Select(UpdateDistanceTravelled)
                    .ToList()
                    .And(AwardWinningPoints)))
            .ToList()
            .Last();

    private static List<RainDeer> AwardWinningPoints(List<RainDeer> raindeers)
        => raindeers.Select(r => r.AccumulatedDistance).Max()
                .And(max => raindeers.Where(r => r.AccumulatedDistance < max)
                    .ToList()
                    .And(list => list.Tap(ls => 
                        ls.AddRange(raindeers
                            .Where(r => r.AccumulatedDistance == max)
                            .Select(r => r with 
                            { 
                                WinningPoints = r.WinningPoints + 1 
                            })))));

    private static RainDeer UpdateDistanceTravelled(RainDeer rd)
        => rd with
            {
                AccumulatedDistance = rd.AccumulatedDistance + (rd.IsResting ? 0 : rd.DistPerSecond),
                IsResting = rd.UntilStateChange - 1 == 0 ? !rd.IsResting : rd.IsResting,
                UntilStateChange = rd.UntilStateChange - 1 == 0
                    ? rd.IsResting ? rd.MaxRun : rd.MaxRest
                    : rd.UntilStateChange - 1
            };
    
    [GeneratedRegex("(?<name>[a-zA-Z]+) can fly (?<speed>[0-9]+) km\\/s for (?<speedburst>[0-9]+) seconds, but then must rest for (?<rest>[0-9]+) seconds.")]
    private static partial Regex ParseInputRegex();
}
