
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public class Solution2023day0002
{
    internal record Draw(int red, int green, int blue);

    internal record Game(int Id)
    {
        public List<Draw> Draws = new List<Draw>();
    }

    public static long SolvePart1(string input)
        => throw new NotImplementedException();
}
