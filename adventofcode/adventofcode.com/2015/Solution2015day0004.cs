
namespace adventofcode.adventofcode.com._2015;

public abstract class Solution2015day0004
{
    public static int Solve(string input, string startsWith, int rangeStart = 1)
        => Enumerable.Range(rangeStart, Int32.MaxValue - rangeStart)
            .TakeWhile(rv =>
            {
                var strToHash = $"{input}{rv}";
                var hash = CreateMD5(strToHash);
                return !hash.StartsWith(startsWith);
            }).Last() + 1;

    // from SO -> https://stackoverflow.com/a/24031467
    private static string CreateMD5(string input)
    {
        // Use input string to calculate MD5 hash
        var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        var hashBytes = System.Security.Cryptography.MD5.HashData(inputBytes);

        return Convert.ToHexString(hashBytes); // .NET 5 +
    }
}
