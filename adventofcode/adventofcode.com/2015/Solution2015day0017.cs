
namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0017
{
    private record Combination(int Combo, int Sum);
    
    public static int SolvePart1(string input, int quantity)
        =>  input
            .Parse()
            .And(containers => ComputeMatchingBarrelCombinations(quantity, containers))
            .Count();

    public static int SolvePart2(string input, int quantity)
        => input
            .Parse()
            .And(containers => ComputeMatchingBarrelCombinations(quantity, containers))
            .And(FilterMinBarrelCombinations)
            .Count();

    private static IEnumerable<Combination> FilterMinBarrelCombinations(IEnumerable<Combination> configurations)
        => configurations
            .MinBy(c => c.Sum)!
            .Combo.GetBits(32).Count(b => b == 1)
            .And(minBarrels => 
                configurations
                    .Where(c => c.Combo.GetBits().Count(b => b == 1) == minBarrels));

    private static IEnumerable<Combination> ComputeMatchingBarrelCombinations(int quantity, List<int> containers)
        => Enumerable.Range(0, GetNumStartingOnes(containers.Count))
            .Select(com =>
                new Combination(
                    Combo: com,
                    Sum: com.GetBits(containers.Count)
                        .Zip(containers, (a, b) => a * b)
                        .Sum()))
            .Where(s => s.Sum == quantity);

    private static List<int> Parse(this string input) 
        => input
            .Split('\n')
            .Select(l => l.Trim().ToInt()).ToList();

    private static IEnumerable<int> GetBits(this int num, int countBits = 20)
        => Enumerable.Range(0, countBits)
            .Select(b => (num & (1 << b)) != 0 ? 1 : 0);

    private static int GetNumStartingOnes(this int count)
        => Enumerable.Range(0, count)
            .Select(b => 1 << b)
            .Aggregate((a, b) => a | b);
}
