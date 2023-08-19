
using System.Numerics;

namespace adventofcode.adventofcode.com._2015;

public class Solution2015day0002
{
    public static long SolvePart1(string input)
        => input
            .Split('\n').Where(l => !string.IsNullOrEmpty(l))
            .Select(l => l.Split('x').Select(long.Parse))
            .Select(w =>
            {
                var f = w.ToList();
                var a = f[0] * f[1];
                var b = f[1] * f[2];
                var c = f[2] * f[0];
                return 2 * a + 2 * b + 2 * c + Math.Min(a, Math.Min(b, c));
            })
            .Aggregate((a, b) => a + b);

    public static long SolvePart2(string input)
        => input
            .Split('\n').Where(l => !string.IsNullOrEmpty(l))
            .Select(l => l.Split('x').Select(long.Parse))
            .Select(w =>
            {
                var f = w.ToList();
                f.Sort();
                return 2 * f[0] + 2 * f[1] + f[0] * f[1] * f[2];
            })
            .Aggregate((a, b) => a + b);
}
