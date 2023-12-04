
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0019
{
    private static HashSet<string> _alreadySeen = new HashSet<string>();
    
    public static long SolvePart1(string input)
        => input.Parse()
            .Map(data => 
                data.Rules.SelectMany(rule => ExpandRuleByKey(data.MedicineMolecule, rule)))
            .Distinct()
            .Count();

    private static IEnumerable<string> ExpandRuleByKey(string medicineMolecule, KeyValuePair<string, string> rule)
        => Regex
            .Matches(medicineMolecule, $"{rule.Key}")
            .Map(matches => matches.Select(match => 
                match.Fork(
                    strings => string.Join("", strings.First(), rule.Value, strings.Last()), 
                    m => medicineMolecule[..m.Index], 
                    m => medicineMolecule[(m.Index + m.Length)..])));

    // Implementation of the solution at https://www.reddit.com/r/adventofcode/comments/3xflz8/comment/cy4etju/?utm_source=share&utm_medium=web2x&context=3
    public static long SolvePart2(string input)
        => input.Parse()
            .Map(data => data.Fork(elems => elems.Sum(), 
                NumberOfParenthesis, NumberOfElements, NumberOfCommasDoubled));

    private static int NumberOfCommasDoubled((string MedicineMolecule, ImmutableList<KeyValuePair<string, string>> Rules) data)
        => -2 * data.MedicineMolecule.Count(c => c == 'Y');

    private static int NumberOfElements((string MedicineMolecule, ImmutableList<KeyValuePair<string, string>> Rules) data)
        =>  data.Rules.Select(e => e.Key).Aggregate((a, b) => $"{a}|{b}")
            .Map(regex => Regex.Match(data.MedicineMolecule, $"^(?<element>({regex}|Ar|Rn|Y|C))+$"))
            .Map(elements => elements.Groups["element"].Captures.Count);

    private static int NumberOfParenthesis((string MedicineMolecule, ImmutableList<KeyValuePair<string, string>> Rules) data)
        => Regex.Matches(data.MedicineMolecule, "(Rn|Ar)").Count
            .Map(numParentheses => -(numParentheses > 0 ? numParentheses + 1 : numParentheses));

    private static (string MedicineMolecule, ImmutableList<KeyValuePair<string, string>> Rules) Parse(this string input)
        => input.Split('\n').Select(l => l.Trim())
            .Map(lines =>
                FunctionalExtensions.Map(lines
                        .TakeWhile(l => !string.IsNullOrEmpty(l))
                        .Select(l => 
                            InputParseRegex()
                                .Match(l)
                                .Map(match => new KeyValuePair<string, string>(match.Groups["key"].Value, match.Groups["value"].Value))), keyValuePairs => (
                        MedicineMolecule: lines.Last(),
                        Rules: new List<KeyValuePair<string, string>>(keyValuePairs.ToList()).ToImmutableList())));
    
    [GeneratedRegex("(?<key>[a-zA-Z]+) => (?<value>[a-zA-Z]+)")]
    private static partial Regex InputParseRegex();
}
