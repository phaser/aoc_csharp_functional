
using System.Dynamic;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.RegularExpressions;
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
        var json = JsonSerializer.Deserialize<ExpandoObject>(input);
        var fo = json.Select(c => c.Value as ExpandoObject).ToList();
    }

    private static string LoadInput()
    {
        var input = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "adventofcode.com/2015/Assets/Testing2015day0012Input.txt"));
        return input;
    }
    
    private const string MainInput = @"{input}";
}
