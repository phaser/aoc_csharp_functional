
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0019
{
    public static long SolvePart1(string input)
        => input.Parse()
            .And(data => 
                data.Rules.SelectMany(rule =>
                    Regex.Matches(data.MedicineMolecule, $"{rule.Key}")
                        .And(matches => matches
                            .Select(match =>
                                data.MedicineMolecule[..match.Index] +
                                rule.Value +
                                data.MedicineMolecule[(match.Index + match.Length)..])))
                    .Distinct()
                    .Count());

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
