
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2023;

public class Solution2023day0002
{
    private record Draw(int red, int green, int blue);

    private record Game(int Id)
    {
        public readonly List<Draw> Draws = new List<Draw>();
    }

    public static long SolvePart1(string input)
        => DecodeInput(input)
            .ToArray()
            .Map(_ => 0);

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
            .Aggregate((a, b) => new Draw(a.red + b.red, a.green + b.green, a.blue + b.blue));

    private static Draw ComponentToDraw(string component) 
        => component.Contains("red")
            ? new Draw(int.Parse(component.Replace("red", "")), 0, 0)
            : component.Contains("green")
                ? new Draw(0, int.Parse(component.Replace("green", "")), 0)
                : new Draw(0, 0, int.Parse(component.Replace("blue", "")));
}
