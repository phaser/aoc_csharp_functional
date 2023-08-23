
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0010
{
    [TestCase("1", 5, 6)]
    [TestCase("1113122113", 40, 360154)]
    [TestCase("1113122113", 50, 5103798)]
    public void TestSolution(string input, int iterations, int expectedResult)
    {
        TestContext.WriteLine($"{input.Length}");
        for (var i = 0; i < iterations; i++)
        {
            TestContext.WriteLine($"Iteration: {i:0000}");
            input = Solution2015day0010.Solve(input);
            TestContext.WriteLine($"{input.Length}");
        }

        input.Length.Should().Be(expectedResult);
    }
}
