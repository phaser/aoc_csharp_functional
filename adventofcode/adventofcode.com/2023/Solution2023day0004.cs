
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public partial class Solution2023day0004
{
    public static long SolvePart1(string input)
        => input.Split("\n")
            .Select(line => line.Split(":")[1]
                .Split("|")
                .Map(numbers => new 
                {
                    WinningNumbers = NumberRegex().Matches(numbers[0]).Select(match => long.Parse(match.Value)).ToList(),
                    CardNumbers = NumberRegex().Matches(numbers[1]).Select(match => long.Parse(match.Value)).ToList()
                })
                .Map(card => card.CardNumbers.Where(card.WinningNumbers.Contains))
                .ToList()
                .Map(winningNumbers => winningNumbers.Count > 0 ? 1 << (winningNumbers.Count() - 1) : 0)
            )
            .Sum();
            
    [GeneratedRegex("[1-9]+[0-9]*")]
    private static partial Regex NumberRegex();
}
