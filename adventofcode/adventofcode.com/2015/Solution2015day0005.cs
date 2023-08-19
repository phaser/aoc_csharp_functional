
namespace adventofcode.adventofcode.com._2015;

public class Solution2015day0005
{
    public static int SolvePart1(string input)
        => input.Split('\n')
            .Select(l =>    !l.Contains("ab")
                         && !l.Contains("cd")
                         && !l.Contains("pq")
                         && !l.Contains("xy")
                         && l.Skip(1).Zip(l, (a, b) => a == b).Any(e => e)
                         && l.Count(c => c is 'a' or 'e' or 'i' or 'o' or 'u') >= 3)
            .Count(elem => elem);
}
