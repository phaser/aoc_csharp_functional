using adventofcode.adventofcode.com._2023;

namespace adventofcode.tests.adventofcode.com._2023;

public class Testing2023day0005
{
    [TestCase(TestInput, 0)]
    [TestCase(MainInput, 0)]
    public void TestSolutionPart1(string input, long expectedResult)
    {
        Solution2023day0005.SolvePart1(input).Should().Be(expectedResult);
    }

    private const string TestInput = @"{input}";    
    private const string MainInput = @"{input}";
}
