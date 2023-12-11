
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public partial class Solution2023day0004
{
    private record Card(int Id, IList<long> WinningNumbers, IList<long> CardNumbers);

    private record ProblemData(Dictionary<int, int> Multipliers, IList<Card> Cards);
    
    public static long SolvePart1(string input)
        => input.Split("\n")
            .Select(line => ParseCard(line)
                .Map(card => card.CardNumbers.Where(card.WinningNumbers.Contains))
                .Map(winningNumbers => winningNumbers.Any() ? 1 << (winningNumbers.Count() - 1) : 0)
            )
            .Sum();

    public static long SolvePart2(string input)
        => input.Split("\n")
            .Select(ParseCard)
            .ToList()
            .Map(cards => new ProblemData(new Dictionary<int, int>(cards.Select(card => new KeyValuePair<int, int>(card.Id, 1))), cards)
            .Map(problemData => problemData.Cards
                .Select(card => card.CardNumbers.Where(card.WinningNumbers.Contains)
                    .ToList()
                    .Map(winningNumbers => Enumerable.Range(1, winningNumbers.Count)
                        .Select(i => problemData.Multipliers.ContainsKey(card.Id + i) 
                            ? problemData.Multipliers[card.Id + i] += problemData.Multipliers[card.Id] 
                            : 0)
                        .ToList()
                        .Map(_ => problemData.Multipliers[card.Id])))))
            .Sum();

    private static Card ParseCard(string line)
        => line.Split(":")
            .Map(parts => parts[1]
                .Split("|")
                .Map(numbers => new Card(
                    Id: int.Parse(parts[0].Replace("Card ", "")),
                    WinningNumbers: NumberRegex().Matches(numbers[0]).Select(match => long.Parse(match.Value)).ToList(), 
                    CardNumbers: NumberRegex().Matches(numbers[1]).Select(match => long.Parse(match.Value)).ToList()
                    )));

    [GeneratedRegex("[1-9]+[0-9]*")]
    private static partial Regex NumberRegex();
}
