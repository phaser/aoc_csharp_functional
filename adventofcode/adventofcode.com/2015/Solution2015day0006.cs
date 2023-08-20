using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public enum Command
{
    On,
    Off,
    Toggle
}

public static class Solution2015day0006
{
    public static int[][] CreateGrid()
        => new int[1000][]
            .Select(row => { return row = new int[1000]; }).ToArray();

    public static string[] ParseCommand(this string command)
        => Regex.Match(command, @"(turn off |turn on |toggle )(\d+),(\d+) through (\d+),(\d+)")
            .And<Match, string[]>(match =>
            {
                if (!match.Success)
                    throw new ArgumentException("command isn't valid");
                var result = new string[5];
                result[0] = match.Groups[1].Value;
                result[1] = match.Groups[2].Value;
                result[2] = match.Groups[3].Value;
                result[3] = match.Groups[4].Value;
                result[4] = match.Groups[5].Value;
                return result;
            });

    public static int SolvePart1(string input)
        => CreateGrid()
            .And(grid => SolveInternal(input, grid, (command, value) => command switch
            {
                "turn on " => 1,
                "turn off " => 0,
                "toggle " => value == 1 ? 0 : 1,
                _ => throw new ArgumentException($"unhdandled {command}")
            }));

    public static int SolvePart2(string input)
        => CreateGrid()
            .And(grid => SolveInternal(input, grid, (command, value) => command switch
            {
                "turn on " => value + 1,
                "turn off " => value > 0 ? value - 1 : value,
                "toggle " => value + 2,
                _ => throw new ArgumentException($"unhdandled {command}")
            }));

    private static int SolveInternal(string input, int[][] grid, Func<string, int, int> process)
        => input.Split('\n')
            .Select(cmd => ProcessCommand(cmd, grid, process))
            .Aggregate((_, b) => b)
            .Select(row => row.Aggregate((a, b) => a + b))
            .Aggregate((a, b) => a + b);

    private static int[][] ProcessCommand(string line, int[][] grid, Func<string, int, int> process)
    {
        var command = line.ParseCommand();
        var sx = int.Parse(command[1]);
        var sy = int.Parse(command[2]);
        var ex = int.Parse(command[3]);
        var ey = int.Parse(command[4]);
        for (int i = sx; i <= ex; i++)
        {
            for (int j = sy; j <= ey; j++)
            {
                grid[i][j] = process(command[0], grid[i][j]);
            }
        }
        return grid;
    }
}
