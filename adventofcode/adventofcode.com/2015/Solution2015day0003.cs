
using System.Drawing;
using System.Runtime.CompilerServices;

namespace adventofcode.adventofcode.com._2015;

public class MinMax
{
    public int Min { get; set; } = 0;
    public int Max { get; set; } = 0;
    public int Pos { get; set; } = 0;
}

public class Solution2015day0003
{
    public static long SolvePart1(string input)
        => SolvePart1Internal(input, new Dictionary<(int Row, int Col), int>()
        {
            { new (0, 0), 0 }
        }, 0, 0, 1);

    public static long SolvePart2(string input)
    {
        var inputSanta = new string(
            input
                .Select((c, idx) => idx % 2 == 0 ? c : (char)0)
                .Where(c => c != 0)
                .ToArray());
        var inputRobo = new string(
            input
                .Select((c, idx) => idx % 2 != 0 ? c : (char)0)
                .Where(c => c != 0)
                .ToArray());
        var dict = new Dictionary<(int Row, int Col), int>()
        {
            { new(0, 0), 0 }
        };

        var countSanta = SolvePart1Internal(inputSanta, dict, 0, 0, 1);
        var countRobo = SolvePart1Internal(inputRobo, dict, 0, 0, 0);
        return countSanta + countRobo;
    }
    
    private static long SolvePart1Internal(string input, Dictionary<(int Row, int Col), int> rows, int crow, int ccol,
        int count)
        => input
            .Select(c =>
            {
                Func<Dictionary<(int Row, int Col), int>, int, int, int> process = c switch
                {
                    '<' or '>' => (Rows, CurrentRow, _) =>
                    {
                        crow = c == '<' ? --CurrentRow : ++CurrentRow;
                        (int Row, int Col) key = new(crow, ccol);
                        return Rows.ContainsKey(key) ? 0 : AddKeyAndReturnOne(Rows, key);
                    },
                    '^' or 'v' => (Rows, _, CurrentCol) =>
                    {
                        ccol = c == '^' ? --CurrentCol : ++CurrentCol;
                        (int Row, int Col) key = new(crow, ccol);
                        return Rows.ContainsKey(key) ? 0 : AddKeyAndReturnOne(Rows, key);
                    }
                };
                return process;
            })
            .Select(f =>
            {
                count += f(rows, crow, ccol);
                return rows;
            })
            .Select(_ => count)
            .Aggregate((a, b) => b);

    private static int AddKeyAndReturnOne(IDictionary<(int Row, int Col), int> Rows, (int Row, int Col) key)
    {
        Rows.Add(key, 0);
        return 1;
    }
}
