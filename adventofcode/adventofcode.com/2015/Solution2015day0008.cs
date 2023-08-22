using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0008
{
    public static int SolvePart1(string[] input)
        => GetStringCodeCount(input) - GetInMemoryCount(input);

    public static int SolvePart2(string[] input)
        => GetEncodedCount(input) - GetStringCodeCount(input);

    private static int GetEncodedCount(string[] input)
        => input
            .Select(s => s.Replace("\\", "\\\\"))
            .Select(s => s.Replace("\"", "\\\""))
            .Select(s => s.Length + 2)
            .Aggregate((a, b) => a + b);

    private static int GetInMemoryCount(string[] input)
        => input
            .Select(s => s.Trim())
            .Select(s => s[1..][..(s.Length - 2)])
            .Select(s => MyRegex().Replace(s, "@"))
            .Select(s => s.Replace("\\\"", "$"))
            .Select(s => s.Replace("\\\\", "%"))
            .Select(s => s.Length)
            .Aggregate((a, b) => a + b);

    private static int GetStringCodeCount(string[] input)
        => input
            .Select(line => line.Length)
            .Aggregate((a, b) => a + b);
    
    [GeneratedRegex("\\\\[x][0-9a-f]{2}")]
    private static partial Regex MyRegex();
}
