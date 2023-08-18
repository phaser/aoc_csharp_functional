using adventofcode.adventofcode.com._2015.day._1;

namespace adventofcode.tests.adventofcode.com._2015.day._1;

public class Testing2015day1
{
    [TestCase(Input2015day1.MainInput, 74)]
    [TestCase(Input2015day1.Input2, 3)]
    public void TestSolutionPart1(string input, long result)
    {
        var solution = new Solution2015day1();
        Solution2015day1.SolvePart1(input).Should().Be(result);
    }

    [TestCase(Input2015day1.MainInput, 1795)]
    [TestCase("()())", 5)]
    public void TestSolutionPart2(string input, long result)
    {
        var solution = new Solution2015day1();
        Solution2015day1.SolvePart2(input).Should().Be(result);
    }
}
