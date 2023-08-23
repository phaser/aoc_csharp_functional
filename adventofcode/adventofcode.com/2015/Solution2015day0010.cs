
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0010
{
    private const double ConwayConstant = 1.3;

    private static string LookAndSay(this string input)
        => string.Concat(
            MyRegex().Match(input)
                .Groups[1].Captures
                .Select(c => c.Length + "" + c.Value[0]));
    
    public static string Solve(string input)
        => input.LookAndSay();
    
    [GeneratedRegex("((.)\\2*)+")]
    private static partial Regex MyRegex();
}
