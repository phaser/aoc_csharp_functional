
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0011
{
    [TestCase("hepxcrrq", "hepxxyzz")]
    [TestCase("abcdefgh", "abcdffaa")]
    [TestCase("ghijklmn", "ghjaabcc")]
    [TestCase("hepxxyzz", "heqaabcc")]
    public void TestSolutionPart1(string password, string expectedNextPassword)
    {
        var nextPassword = Solution2015day0011.SolvePart1(password.ToArray());
        (new string(nextPassword)).Should().Be(expectedNextPassword);
    }

    [TestCase("hijklmmn", false)]
    [TestCase("abbceffg", false)]
    [TestCase("abbcegjk", false)]
    [TestCase("abcdffaa", true)]
    [TestCase("hepxxxyz", false)]
    [TestCase("ghjaabcc", true)]
    [TestCase("abcdffaa", true)]
    public void TestIsValid(string password, bool expectedResult)
    {
        Solution2015day0011.IsValid(password.ToArray()).Should().Be(expectedResult);
    }
    
    private const string MainInput = @"{input}";
}
