
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0014
{
    public record RainDeer(string Name, bool IsResting, int UntilStateChange, int MaxRun, int MaxRest, int DistPerSecond, int Acc, int Pp);

    public record Race(List<RainDeer> Raindeers);

    public static int SolvePart2(string input, int duration)
        => input.Split('\n').Select(s => s.Trim())
            .Select(l => Regex.Match(l, @"(?<name>[a-zA-Z]+) can fly (?<speed>[0-9]+) km\/s for (?<speedburst>[0-9]+) seconds, but then must rest for (?<rest>[0-9]+) seconds.")
                .And(match => new RainDeer(
                    match.Groups["name"].Value,
                    false, 
                    int.Parse(match.Groups["speedburst"].Value),
                    int.Parse(match.Groups["speedburst"].Value),
                    int.Parse(match.Groups["rest"].Value),
                            int.Parse(match.Groups["speed"].Value),
                    0, 0)))
            .And(raindeers => new Race(raindeers.ToList()))
            .And(race => EvalState(race, duration))
            .And(l => l.Raindeers.MaxBy(ra => ra.Pp)!.Pp);

    private static Race EvalState(Race race, int duration)
    {
        while (true)
        {
            if (duration == 0) return race;

            var newRace = race with
            {
                Raindeers = race.Raindeers
                    .Select(rd => rd with {
                        Acc = rd.Acc + (rd.IsResting ? 0 : rd.DistPerSecond),
                        IsResting = (rd.UntilStateChange - 1 == 0 ? !rd.IsResting : rd.IsResting),
                        UntilStateChange = (rd.UntilStateChange - 1 == 0
                            ? rd.IsResting ? rd.MaxRun : rd.MaxRest
                            : (rd.UntilStateChange - 1)) 
                    }).ToList()
                    .And(raindeers => raindeers.Select(r => r.Acc).Max()
                        .And(max => raindeers.Where(r => r.Acc < max)
                            .ToList()
                            .And(list => 
                            { 
                                list.AddRange(raindeers.Where(r => r.Acc == max).Select(r => r with 
                                { 
                                    Pp = r.Pp + 1 
                                })); 
                                return list; 
                            })))
            };
            race = newRace;
            duration = duration - 1;
        }
    }
}
