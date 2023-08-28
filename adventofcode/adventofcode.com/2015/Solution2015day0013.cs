
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0013
{
    private record GainRelation(string Character, string GainsFrom, int Gain);

    private record State(List<GainRelation> Relations, List<string> Participants);

    public static int SolvePart1(string input)
        => ParseInput(input)
            .And(CreateState)
            .And(ExtractListOfParticipants)
            .And(state => state.Participants.ToArray()
                .Permutations()
                .Select(p => p.Zip(p.Skip(1).Append(p[0]), (a, b) =>
                {
                    var r1 = state.Relations.First(r => r.Character == a && r.GainsFrom == b);
                    var r2 = state.Relations.First(r => r.Character == b && r.GainsFrom == a);
                    return r1.Gain + r2.Gain;
                }).Aggregate((a, b) => a + b)))
            .Max();

    public static int SolvePart2(string input)
        => ParseInput(input)
            .And(CreateState)
            .And(ExtractListOfParticipants)
            .And(state => state.Participants.Append("Me").ToArray()
                .Permutations()
                .Select(p => p.Zip(p.Skip(1).Append(p[0]), (a, b) =>
                {
                    if (a == "Me" || b == "Me")
                        return 0;
                    var r1 = state.Relations.First(r => r.Character == a && r.GainsFrom == b);
                    var r2 = state.Relations.First(r => r.Character == b && r.GainsFrom == a);
                    return r1.Gain + r2.Gain;
                }).Aggregate((a, b) => a + b)))
            .Max();

    private static State ExtractListOfParticipants(State state) 
        => state with
        {
            Participants = state.Relations
                .SelectMany(r => new[] {r.Character, r.GainsFrom })
                .Distinct().ToList()
        };

    private static State CreateState(IEnumerable<GainRelation> relations) 
        => new(relations.ToList(), new List<string>());

    private static IEnumerable<GainRelation> ParseInput(string input) 
        => input.Split("\n")
            .Select(l => l.Trim())
            .Select(l => ParseInputRegex().Match(l)
                .And(match => new GainRelation(
                    match.Groups["name"].Value,
                    match.Groups["nextname"].Value,
                    (match.Groups["gain"].Value == "gain" ? 1 : -1) * int.Parse(match.Groups["points"].Value))));
    
    [GeneratedRegex("(?<name>[a-zA-Z]+) would (?<gain>gain|lose) (?<points>[0-9]+) happiness units by sitting next to (?<nextname>[a-zA-Z]+).")]
    private static partial Regex ParseInputRegex();
}
