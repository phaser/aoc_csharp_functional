
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public partial class Solution2023day0003
{
    private record ProblemData(IList<AcceptedRange> AcceptedRanges, IList<Number> Numbers);
    
    private record AcceptedRange(int Line, int Start)
    {
        public int End => Start + 2;
    }

    private record Number(string Num, int Line, int Start)
    {
        public int End => Start + Num.Length - 1;
    }

    public static long SolvePart1(string input)
        => ExtractAcceptedRanges(input)
            .Map(ExtractNumbers(input))
            .Map(Solve);

    private static long Solve(ProblemData problemData)
        => problemData.Numbers.Where(num =>
                problemData.AcceptedRanges.Any(range => range.Line == num.Line && (
                    (range.Start >= num.Start && range.Start <= num.End) ||
                    (range.End >= num.Start && range.End <= num.End))))
            .Select(num => long.Parse(num.Num))
            .Sum();

    private static Func<ProblemData, ProblemData> ExtractNumbers(string input)
    => problemData => input
            .Split("\n")
            .Select((line, lineIdx) => new
            {
                LineIdx = lineIdx,
                Matches = MyRegex().Matches(line)
            })
            .SelectMany(m => m.Matches.Select(match => new Number(match.Value, m.LineIdx, match.Index)))
            .Map(numbers => problemData with
            {
                Numbers = numbers.ToList()
            });
    
    private static ProblemData ExtractAcceptedRanges(string input)
        => input.Split("\n")
            .Select((line, lineIdx) => line.Select((c, idx) => char.IsDigit(c) || c == '.' 
                    ? Array.Empty<AcceptedRange>().ToList()
                    : new List<AcceptedRange>()
                    {
                        new AcceptedRange(lineIdx - 1, idx - 1),
                        new AcceptedRange(lineIdx, idx - 1),
                        new AcceptedRange(lineIdx + 1, idx - 1)
                    })
                .Where(ranges => ranges.Count > 0)
                .SelectMany(a => a.AsEnumerable())
                .ToList())
            .SelectMany(a => a)
            .Map(acceptedRangesList => new ProblemData(acceptedRangesList.ToList(), Array.Empty<Number>()));
    [GeneratedRegex("[1-9]+[0-9]*")]
    private static partial Regex MyRegex();
}
