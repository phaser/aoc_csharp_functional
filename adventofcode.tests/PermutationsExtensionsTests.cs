namespace adventofcode.tests.adventofcode.com._2015;

public class PermutationsExtensionsTests
{
    [TestCase]
    public void Test()
    {
        var nums = new int[] { 1, 2, 3 };
        var perms = nums.Permutations().ToList();
    }
}