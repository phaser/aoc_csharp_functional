
using System.Reflection;
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0008
{
    [TestCase]
    public void TestSolutionPart1()
    {
        var lines = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "adventofcode.com/2015/Assets/Testing2015day0008Input.txt"));
        Solution2015day0008.SolvePart1(lines).Should().Be(1333);
    }
    
    [TestCase]
    public void TestSolutionPart1OnTestInput()
    {
        var lines = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "adventofcode.com/2015/Assets/Testing2015day0008TestInput.txt"));
        Solution2015day0008.SolvePart1(lines).Should().Be(12);
    }

    [TestCase]
    public void TestSolutionPart2()
    {
        var lines = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "adventofcode.com/2015/Assets/Testing2015day0008Input.txt"));
        Solution2015day0008.SolvePart2(lines).Should().Be(2046);
    }

    [TestCase]
    public void TestSolutionPart2OnTestInput()
    {
        var lines = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "adventofcode.com/2015/Assets/Testing2015day0008TestInput.txt"));
        Solution2015day0008.SolvePart2(lines).Should().Be(19);
    }
}
