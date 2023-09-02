
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0020
{
    [TestCase]
    public void TestSolutionPart1()
    {
        Solution2015day0020.Solve(33100000, 10, 33100000).Should().Be(776160);
    }

    [TestCase]
    public void TestSolutionPart2()
    {
        Solution2015day0020.Solve(33100000, 11, 50).Should().Be(786240);
    }

    [TestCase]
    public void TestSolutionPart1Iterative()
    {
        Solution2015day0020.SolveIterative(33100000, 10, 33100000).Should().Be(776160);
    }

    [TestCase]
    public void TestSolutionPart2Iterative()
    {
        Solution2015day0020.SolveIterative(33100000, 11, 50).Should().Be(786240);
    }
}
