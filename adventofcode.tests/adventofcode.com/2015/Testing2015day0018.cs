
using adventofcode.adventofcode.com._2015;

namespace adventofcode.tests.adventofcode.com._2015;

public class Testing2015day0018
{
    [TestCase]
    public void TestSolution()
    {
    }

    [TestCase]
    public void TestBoardImplementation()
    {
        var board = new Board(100, 100);
        var listOfCoords = new List<(int X, int Y, int Value)>()
        {
            new(0, 0, 97),
            new(99, 99, 123),
            new(50, 50, 312),
            new(51, 51, 213),
            new(67, 3, 875),
            new(3, 67, 977),
        };
        foreach (var coord in listOfCoords)
        {
            board.Set(coord.X, coord.Y, coord.Value).Get(coord.X, coord.Y).Should().Be(coord.Value);
        }
    }
    
    private const string MainInput = @"{input}";
}
