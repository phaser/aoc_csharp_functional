using System.Text.RegularExpressions;

namespace adventofcode;

public static class StringsExtensions
{
    public static int Count(this string input, string substr)
    {
        return Regex.Matches(input, substr).Count;
    }
}