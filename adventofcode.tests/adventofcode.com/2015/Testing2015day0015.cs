
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0015
{
    [TestCase(TestInput, 62842880)]
    [TestCase(MainInput, 13882464)]
    public void TestSolutionPart1(string input, long expectedScore)
    {
        Solution2015day0015.SolvePart1(input).Should().Be(expectedScore);
    }

    [TestCase(TestInput, 57600000)]
    [TestCase(MainInput, 11171160)]
    public void TestSolutionPart2(string input, long expectedScore)
    {
        Solution2015day0015.SolvePart2(input).Should().Be(expectedScore);
    }

    private const string MainInput = @"Sprinkles: capacity 5, durability -1, flavor 0, texture 0, calories 5
PeanutButter: capacity -1, durability 3, flavor 0, texture 0, calories 1
Frosting: capacity 0, durability -1, flavor 4, texture 0, calories 6
Sugar: capacity -1, durability 0, flavor 0, texture 2, calories 8";

    private const string TestInput = @"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";
}
