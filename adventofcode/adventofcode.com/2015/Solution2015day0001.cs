
namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day1Extensions
{
    internal static long SolvePart2FindIndex(this IEnumerable<dynamic> values, long csum)
        => values.TakeWhile(v =>
            {
                csum += v.Value;
                return csum != -1;
            })
            .Last().Index + 1;
}

public class Solution2015day0001
{
    public static long SolvePart1(string input)
        => input
            .Select(c => (long)(c == '(' ? 1 : -1))
            .Aggregate((a, b) => a + b);

    public static long SolvePart2(string input)
        => input
            .Select((c, idx) => c == '('
                ? new { Value = (long)1, Index = idx + 1 }
                : new { Value = (long)-1, Index = idx + 1 })
            .SolvePart2FindIndex(0);
}
