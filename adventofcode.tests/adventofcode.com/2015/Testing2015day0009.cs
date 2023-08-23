
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0009
{
    [TestCase(TestInput1, 605)]
    [TestCase(MainInput, 117)]
    public void TestSolutionPart1(string input, int distance)
    {
        Solution2015day0009.SolvePart1(input).Should().Be(distance);
    }

    [TestCase(MainInput, 909)]
    public void TestSolutionPart2(string input, int distance)
    {
        Solution2015day0009.SolvePart2(input).Should().Be(distance);
    }
    
    private const string TestInput1 = @"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";
    
    private const string MainInput = @"Faerun to Tristram = 65
Faerun to Tambi = 129
Faerun to Norrath = 144
Faerun to Snowdin = 71
Faerun to Straylight = 137
Faerun to AlphaCentauri = 3
Faerun to Arbre = 149
Tristram to Tambi = 63
Tristram to Norrath = 4
Tristram to Snowdin = 105
Tristram to Straylight = 125
Tristram to AlphaCentauri = 55
Tristram to Arbre = 14
Tambi to Norrath = 68
Tambi to Snowdin = 52
Tambi to Straylight = 65
Tambi to AlphaCentauri = 22
Tambi to Arbre = 143
Norrath to Snowdin = 8
Norrath to Straylight = 23
Norrath to AlphaCentauri = 136
Norrath to Arbre = 115
Snowdin to Straylight = 101
Snowdin to AlphaCentauri = 84
Snowdin to Arbre = 96
Straylight to AlphaCentauri = 107
Straylight to Arbre = 14
AlphaCentauri to Arbre = 46";
}
