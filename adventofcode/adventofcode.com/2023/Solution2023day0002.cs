
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public class Solution2023day0002
{
    private record Draw(int Red, int Green, int Blue);

    private record Game(int Id)
    {
        public readonly List<Draw> Draws = new List<Draw>();
    }

    public static long SolvePart1(string input)
        => DecodeInput(input)
            .Where(game => IsValid(game, new Draw(12, 13, 14)))
            .Select(game => game.Id)
            .Sum();

    public static long SolvePart2(string input)
        => DecodeInput(input)
            .Select(PowerSet)
            .Select(powerSet => (long) powerSet.Red * powerSet.Green * powerSet.Blue)
            .Sum();

    private static Draw PowerSet(Game game)
        => game.Draws.Aggregate((a, b) =>
            new Draw(Math.Max(a.Red, b.Red), Math.Max(a.Green, b.Green), Math.Max(a.Blue, b.Blue)));

    private static bool IsValid(Game game, Draw total)
        => !game.Draws.Any(game => game.Red > total.Red || game.Blue > total.Blue || game.Green > total.Green);

    private static IEnumerable<Game> DecodeInput(string input)
        => input.Split("\n")
            .Select(line => line.Split(":")
                .Map(gameArr =>
                    new Tuple<Game, string[]>(new Game(int.Parse(gameArr[0].Replace("Game ", ""))), gameArr))
                .Map(game => game.Item2[1].Split(";")
                    .Select(drawStr => DecodeDraw(drawStr, game))
                    .Map(draws =>
                    {
                        game.Item1.Draws.AddRange(draws);
                        return game.Item1;
                    })));

    private static Draw DecodeDraw(string drawStr, Tuple<Game, string[]> game)
        => drawStr.Split(",")
            .Select(ComponentToDraw)
            .Aggregate((a, b) => new Draw(a.Red + b.Red, a.Green + b.Green, a.Blue + b.Blue));

    private static Draw ComponentToDraw(string component) 
        => component.Contains("red")
            ? new Draw(int.Parse(component.Replace("red", "")), 0, 0)
            : component.Contains("green")
                ? new Draw(0, int.Parse(component.Replace("green", "")), 0)
                : new Draw(0, 0, int.Parse(component.Replace("blue", "")));
}
