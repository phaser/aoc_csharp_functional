
using System.Numerics;
using adventofcode.adventofcode.com._2015.day._2;

namespace adventofcode.tests.adventofcode.com._2015.day._2;

public class Testing2015day2
{
    [TestCase(Input2015day2.MainInput, 1606483)]
    public void TestSolutionPart1(string input, long result)
    {
        Solution2015day2.SolvePart1(input).Should().Be(result);
    }

    [TestCase(Input2015day2.MainInput, 3842356)]
    public void TestSolutionPart2(string input, long result)
    {
        Solution2015day2.SolvePart2(input).Should().Be(result);
    }
}
