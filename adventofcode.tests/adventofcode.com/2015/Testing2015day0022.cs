
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0022
{
    [TestCase]
    public void TestWhoWins()
    {
        var result = Solution2015day0022.WhoWins(
            new Solution2015day0022.Player(10, 250),
            new int[] { 3, 0 },
            new Solution2015day0022.Player(13, 0, 8));
        result.Winner.Should().Be(0);
    }

    private const string MainInput = @"{input}";
}
