using System.Reflection;
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0012
{
    [TestCase]
    public void TestSolutionPart1()
    {
        var input = LoadInput();
        Solution2015day0012.SolvePart1(input).Should().Be(111754);
    }

    [TestCase]
    public void TestSolutionPart2()
    {
        var input = LoadInput();
        Solution2015day0012.SolvePart2(input).Should().Be(65402);
    }

    private static string LoadInput()
    {
        var input = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "adventofcode.com/2015/Assets/Testing2015day0012Input.txt"));
        return input;
    }
}
