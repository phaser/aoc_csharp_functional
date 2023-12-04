using static System.Linq.Enumerable;

namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0011
{
    public static char[] SolvePart1(char[] currentPassword)
        => Range(0, int.MaxValue)
            .TakeWhile(_ =>
            {
                Increment(currentPassword);
                return !IsValid(currentPassword);
            }).Aggregate((a, b) => b)
            .Map(_ => currentPassword);

    public static bool IsValid(char[] currentPassword)
        => !currentPassword.Any(c => c is 'i' or 'o' or 'l')
           && currentPassword.Zip<char, char, string>(currentPassword.Skip(1), (a, b) => a + "" + b)
               .Zip<string, char, string>(currentPassword.Skip(2), (a, b) => a + b)
               .Any(seq => seq[0] == seq[1] - 1 && seq[1] == seq[2] - 1)
           && HasTwoNonOverlappingPairs(currentPassword);

    private static bool HasTwoNonOverlappingPairs(IEnumerable<char> currentPassword)
        => currentPassword.Select((c, idx) => new { C = c, I = idx })
            .Map(ns => 
                ns.Zip(ns.Skip(1), (a, b) => a.C == b.C ? new { I1 = a.I, I2 = b.I } : null)
                  .Where(p => p is not null))
            .Map(BuildHashSet!)
            .Map(hs => hs.Count / 2 > 1);

    private static HashSet<int> BuildHashSet(IEnumerable<dynamic> ns)
        => new HashSet<int>()
            .Map(hs => ns.Select(p =>
                {
                    hs.Add(p!.I1);
                    hs.Add(p.I2);
                    return 0;
                }).ToList()
                .Map(_ => hs));

    private static void Increment(char[] currentPassword)
        => new[] { 7, 6, 5, 4, 3, 2, 1, 0 }
            .TakeWhile(idx =>
            {
                var condition = (++currentPassword[idx] > 122);
                currentPassword[idx] = condition ? (char)97 : currentPassword[idx];
                return condition;
            })
            .ToList();
}
