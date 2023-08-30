
namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0017
{
    public static int SolvePart1(string intput, int quantity)
        => ParseInput(intput)
            .And(containers =>
                Enumerable.Range(0, GetNumStartingOnes(containers.Count))
                    .Select(com => 
                        com.GetBits(containers.Count)
                        .Zip(containers, (a, b) => a * b)
                        .Sum())
                    .Where(s => s == quantity))
            .Count();

    private static List<int> ParseInput(string input) 
        => input
            .Split('\n')
            .Select(l => l.Trim().ToInt()).ToList();

    private static IEnumerable<int> GetBits(this int num, int countBits = 20)
        => Enumerable.Range(0, countBits)
            .Select(b => (num & (1 << b)) != 0 ? 1 : 0);

    private static int GetNumStartingOnes(this int count)
        => Enumerable.Range(0, count)
            .Select(b => (1 << b))
            .Aggregate((a, b) => a | b);
}
