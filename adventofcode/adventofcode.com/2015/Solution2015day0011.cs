
using System.Security.Cryptography;

namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0011
{
    public static char[] SolvePart1(char[] currentPassword)
    {
        do
        {
            Increment(currentPassword);
        } while (!IsValid(currentPassword));

        return currentPassword;
    }

    public static bool IsValid(char[] currentPassword)
        => !currentPassword.Any(c => c is 'i' or 'o' or 'l')
           && currentPassword.Zip<char, char, string>(currentPassword.Skip(1), (a, b) => a + "" + b)
               .Zip<string, char, string>(currentPassword.Skip(2), (a, b) => a + b)
               .Any(seq => seq[0] == seq[1] - 1 && seq[1] == seq[2] - 1)
           && HasTwoNonOverlappingPairs(currentPassword);

    private static bool HasTwoNonOverlappingPairs(char[] currentPassword)
    {
        var list = new List<int>();
        for (var i = 1; i < currentPassword.Length; i++)
        {
            if (currentPassword[i] == currentPassword[i - 1] && !list.Contains(i - 1))
                list.Add(i);
        }

        return list.Count > 1;
    }

    private static void Increment(char[] currentPassword)
    {
        for (var i = 7; i >= 0; i--)
        {
            currentPassword[i]++;
            if (currentPassword[i] <= 122)
                break;
            currentPassword[i] = (char)97;
        }
    }
}
