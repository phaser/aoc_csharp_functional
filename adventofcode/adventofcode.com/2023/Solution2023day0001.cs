
using System.Text;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public class Solution2023day0001
{
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
