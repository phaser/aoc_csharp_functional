
namespace adventofcode.adventofcode.com._2015;

public class Solution2015day0005
{
    public static char[] Alpha = "abcdefghijklmnopqrstuvxyzwa".ToCharArray();
    public static IEnumerable<char> Letters = Alpha.AsEnumerable();
    
    public static int SolvePart1(string input)
        => input.Split('\n')
            .Select(l =>    !l.Contains("ab")
                         && !l.Contains("cd")
                         && !l.Contains("pq")
                         && !l.Contains("xy")
                         && l.Skip(1).Zip(l, (a, b) => a == b).Any(e => e)
                         && l.Count(c => c is 'a' or 'e' or 'i' or 'o' or 'u') >= 3)
            .Count(elem => elem);

    public static int SolvePart2(string input)
        => input.Split('\n')
            .Select(l =>
                   l.Zip(l.Skip(1), (a, b) => a + "" + b)
                    .Select(zp => l.Count(zp) > 1).Any(e => e)
                && l.Zip(l.Skip(2), (a, b) => a == b).Any(e => e))
            .Count(e => e);
}
