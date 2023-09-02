
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0020
{
    [TestCase]
    public void TestSolution()
    {
        Solution2015day0020.SolvePart1(33100000).Should().Be(776160);
    }

    [TestCase]
    public void TestSolutionPart1Iterative()
    {
        Solution2015day0020.SolvePart1Iterative(33100000).Should().Be(776160);
    }
}
