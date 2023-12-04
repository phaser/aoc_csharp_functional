
using adventofcode.adventofcode.com._2023;

namespace adventofcode.tests.adventofcode.com._2023;

public class Testing2023day0002
{
    [TestCase(TestInput, 73)]
    public void TestSolution(string input, long answer)
    {
        Solution2023day0002.SolvePart1(input).Should().Be(answer);
    }

    private const string TestInput = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
    
    private const string MainInput = @"{input}";
}
