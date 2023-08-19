
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0004
{
    [TestCase(MainInput, 117946)]
    [TestCase(Input1, 609043)]
    public void TestSolutionPart1(string input, int answer)
    {
        Solution2015day0004.Solve(input, "00000").Should().Be(answer);
    }

    [TestCase(MainInput, 3938038, 117946)]
    [TestCase(Input1, 6742839, 609043)]
    public void TestSolutionPart2(string input, int answer, int rangeStart)
    {
        Solution2015day0004.Solve(input, "000000", rangeStart).Should().Be(answer);
    }

    private const string MainInput = "ckczppom";
    private const string Input1 = "abcdef";
}
