
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0021
{
    [TestCase]
    public void TestSolutionPart1()
    {
        Solution2015day0021.SolvePart1(new Solution2015day0021.Player(103, 9, 2)).Should().Be(121);
    }
    
    [TestCase]
    public void TestSolutionPart2()
    {
        Solution2015day0021.SolvePart2(new Solution2015day0021.Player(103, 9, 2)).Should().Be(201);
    }

    [TestCase]
    public void TestWhoWins()
    {
        var winner = Solution2015day0021.WhoWins(new Solution2015day0021.Player(8, 5, 5),
            new Solution2015day0021.Player(12, 7, 2));
        winner.Should().Be(0);
    }

    private const string MainInput = @"{input}";
}
