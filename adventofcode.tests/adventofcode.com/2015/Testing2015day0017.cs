
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0017
{
    [TestCase(Testing, 25, 4)]
    [TestCase(MainInput, 150, 654)]
    public void TestSolution(string input, int quantity, int expectedResult)
    {
        Solution2015day0017.SolvePart1(input, quantity).Should().Be(expectedResult);
    }

    private const string Testing = @"20
15
10
5
5";
    
    private const string MainInput = @"50
44
11
49
42
46
18
32
26
40
21
7
18
43
10
47
36
24
22
40";
}
