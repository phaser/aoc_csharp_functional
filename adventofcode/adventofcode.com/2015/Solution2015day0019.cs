
using System.Collections.Immutable;
using System.Formats.Asn1;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0019
{
    public static long SolvePart1(string input)
        => input.Parse()
            .And(data => 
                data.Rules.SelectMany(rule => ExpandRule(data.MedicineMolecule, rule)))
            .Distinct()
            .Count();

    private static IEnumerable<string> ExpandRule(string medicineMolecule, KeyValuePair<string, string> rule)
        => Regex
            .Matches(medicineMolecule, $"{rule.Key}")
            .And(matches => matches.Select(match => 
                match.Fork(
                    strings => string.Join("", strings.First(), rule.Value, strings.Last()), 
                    m => medicineMolecule[..m.Index], 
                    m => medicineMolecule[(m.Index + m.Length)..])));

    public static long SolvePart2(string input)
        => input.Parse()
            .And(data =>
            {
                var currentMolecules = data.Rules
                    .Where(r => r.Key == "e")
                    .Select(r => r.Value)
                    .ToList();
                var steps = 1;
                while (true)
                {
                    var newMolecules = currentMolecules
                        .SelectMany(str => data.Rules
                            .SelectMany(rule => ExpandRule(str, rule)))
                        .Where(molecule => molecule.Length <= data.MedicineMolecule.Length)
                        .ToList();
                    steps++;
                    if (newMolecules.Any(molecule => molecule == data.MedicineMolecule))
                        break;
                    currentMolecules = newMolecules;
                }
                return steps;
            });
    
    private static (string MedicineMolecule, ImmutableList<KeyValuePair<string, string>> Rules) Parse(this string input)
        => input.Split('\n').Select(l => l.Trim())
            .And(lines =>
                lines
                    .TakeWhile(l => !string.IsNullOrEmpty(l))
                    .Select(l => 
                        InputParseRegex()
                            .Match(l)
                            .And(match => new KeyValuePair<string, string>(match.Groups["key"].Value, match.Groups["value"].Value)))
                    .And(keyValuePairs => (
                        MedicineMolecule: lines.Last(),
                        Rules: new List<KeyValuePair<string, string>>(keyValuePairs.ToList()).ToImmutableList())));
    
    [GeneratedRegex("(?<key>[a-zA-Z]+) => (?<value>[a-zA-Z]+)")]
    private static partial Regex InputParseRegex();
}
