
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public partial class Solution2015day0012
{
    public static int SolvePart1(string input)
        => MatchNumberRegex().Matches(input)
            .Select(m => int.Parse(m.Value))
            .Aggregate((a, b) => a + b);
    
    [GeneratedRegex("-?[0-9]+")]
    private static partial Regex MatchNumberRegex();
}
