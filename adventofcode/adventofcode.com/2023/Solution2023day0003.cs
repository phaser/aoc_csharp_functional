using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public partial class Solution2023day0003
{
    private record ProblemData(IList<AcceptedRange> AcceptedRanges, IList<Number> Numbers);
    
    private record AcceptedRange(int Line, int Start)
    {
        public int End => Start + 2;
        public int X { get; set; }
        public int Y { get; set; }
    }

    private record Number(string Num, int Line, int Start)
    {
        private long _num = -1;
        public int End => Start + Num.Length - 1;

        public long NumValue => _num = _num == -1 ? long.Parse(Num) : _num;
    }

    public static long SolvePart1(string input)
        => ExtractAcceptedRanges(input, IsSymbolPart1)
            .Map(ExtractNumbers(input))
            .Map(SolvePart1Internal);

    public static long SolvePart2(string input)
        => ExtractAcceptedRanges(input, IsSymbolPart2)
            .Map(ExtractNumbers(input))
            .Map(SolvePart2Internal);
    
    private static long SolvePart1Internal(ProblemData problemData)
        => problemData.Numbers
            .Where(num => IsNumberNearASymbol(problemData.AcceptedRanges, num))
            .Select(num => num.NumValue)
            .Sum();

    private static long SolvePart2Internal(ProblemData problemData)
        => problemData.AcceptedRanges
            .GroupBy(range => new { range.X, range.Y })
            .Select(wheel => problemData.Numbers
                .Where(num => IsNumberNearASymbol(wheel, num))
                .Select(num => num.NumValue)
                .ToList()
                .Map(nums => nums.Count > 1 // a wheel has at least two parts
                    ? nums.Aggregate((a, b) => a * b)
                    : 0))
            .Sum();
    
    private static bool IsNumberNearASymbol(IEnumerable<AcceptedRange> ranges, Number num)
        => ranges.Any(range => IsRangeIntersectingNumber(range, num));

    private static bool IsRangeIntersectingNumber(AcceptedRange range, Number num)
        => range.Line == num.Line && (
            (range.Start >= num.Start && range.Start <= num.End) ||
            (range.End >= num.Start && range.End <= num.End) ||
            (range.Start <= num.Start && range.End >= num.End));
    
    private static Func<ProblemData, ProblemData> ExtractNumbers(string input)
    => problemData => input
            .Split("\n")
            .Select((line, lineIdx) => new
            {
                LineIdx = lineIdx,
                Matches = NumberRegex().Matches(line)
            })
            .SelectMany(m => m.Matches.Select(match => new Number(match.Value, m.LineIdx, match.Index)))
            .Map(numbers => problemData with
            {
                Numbers = numbers.ToList()
            });
    
    private static ProblemData ExtractAcceptedRanges(string input, Func<char, bool> isSymbol)
        => input.Split("\n")
            .Select((line, lineIdx) => line.Select((c, idx) => isSymbol(c)
                    ? new List<AcceptedRange>()
                    {
                        new(lineIdx - 1, idx - 1) { X = idx, Y = lineIdx },
                        new(lineIdx, idx - 1) { X = idx, Y = lineIdx },
                        new(lineIdx + 1, idx - 1) { X = idx, Y = lineIdx }
                    }
                    : Array.Empty<AcceptedRange>().ToList())
                .Where(ranges => ranges.Count > 0)
                .SelectMany(a => a.AsEnumerable())
                .ToList())
            .SelectMany(a => a)
            .Map(acceptedRangesList => new ProblemData(acceptedRangesList.ToList(), Array.Empty<Number>()));

    private static bool IsSymbolPart1(char c) => !(char.IsDigit(c) || c == '.' || c == '\n' || c == '\r');
    
    private static bool IsSymbolPart2(char c) => c == '*';

    [GeneratedRegex("[1-9]+[0-9]*")]
    private static partial Regex NumberRegex();
}
