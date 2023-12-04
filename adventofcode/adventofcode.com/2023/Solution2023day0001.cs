
using System.Text;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public class Solution2023day0001
{
    private static readonly Dictionary<string, string> Replacements = new()
    {
        { "one",   "1" },
        { "two",   "2" },
        { "three", "3" },
        { "four",  "4" },
        { "five",  "5" },
        { "six",   "6" },
        { "seven", "7" },
        { "eight", "8" },
        { "nine",  "9" },
        { "1", "1" },
        { "2", "2" },
        { "3", "3" },
        { "4", "4" },
        { "5", "5" },
        { "6", "6" },
        { "7", "7" },
        { "8", "8" },
        { "9", "9" },
    };
    
    public static long SolveDay1A(string input)
        => input.Split("\n")
            .Select(line =>
                line.Where(char.IsDigit)
                    .ToArray()
                    .Map(da => da.Length > 0 ? long.Parse("" + da[0] + da[^1]) : 0))
            .Sum();

    public static long SolveDay1B(string input)
        => input.Split("\n")
            .Select(line => new StringBuilder(line)
                .Replace("one", "one1one")
                .Replace("two", "two2two")
                .Replace("three", "three3three")
                .Replace("four", "four4four")
                .Replace("five", "five5five")
                .Replace("six", "six6six")
                .Replace("seven", "seven7seven")
                .Replace("eight", "eight8eight")
                .Replace("nine", "nine9nine")
                .ToString()
                .Where(char.IsDigit)
                .ToArray()
                .Map(da => da.Length > 0 ? long.Parse("" + da[0] + da[^1]) : 0))
            .Sum();
}
