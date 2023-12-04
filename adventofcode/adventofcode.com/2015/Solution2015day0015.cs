using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public static partial class Solution2015day0015
{
    public record Ingredient(string Name, int Capacity, int Durability, int Flavor, int Texture, int Calories);

    public static long SolvePart1(string input)
        => ParseInput(input)
            .Map(ingredients => 
                GenerateCombinationsOfTeaspoons(ingredients.Count)
                    .Select(combo => ComputeComboScore(combo, ingredients))
                    .Select(AdjustNegativeCombos)
                    .Select(AggregateFinalScore)
                    .Max());

    public static long SolvePart2(string input)
        => ParseInput(input)
            .Map(ingredients => 
                GenerateCombinationsOfTeaspoons(ingredients.Count)
                    .Select(combo => ComputeComboScore(combo, ingredients))
                    .Where(FilterNon500CaloriesCombos)
                    .Select(AdjustNegativeCombos)
                    .Select(AggregateFinalScore)
                    .Max());

    private static bool FilterNon500CaloriesCombos(Ingredient ingredient)
        => ingredient.Calories == 500;

    private static long AggregateFinalScore(Ingredient i) => i.Capacity * (long)i.Durability * i.Flavor * i.Texture;

    private static Ingredient AdjustNegativeCombos(Ingredient ingredient)
        => ingredient with
        {
            Capacity = ingredient.Capacity < 0 ? 0 : ingredient.Capacity,
            Durability = ingredient.Durability < 0 ? 0 : ingredient.Durability,
            Flavor = ingredient.Flavor < 0 ? 0 : ingredient.Flavor,
            Texture = ingredient.Texture < 0 ? 0 : ingredient.Texture,
            Calories = ingredient.Calories < 0 ? 0 : ingredient.Calories
        };

    private static Ingredient ComputeComboScore(IList<int> combo, List<Ingredient> ingredients) 
        => combo.Select((e, idx) => 
            new Ingredient("", 
                Capacity: e * ingredients[idx].Capacity, 
                Durability: e * ingredients[idx].Durability, 
                Flavor: e * ingredients[idx].Flavor, 
                Texture: e * ingredients[idx].Texture, 
                Calories: e * ingredients[idx].Calories
                ))
            .Aggregate((a, b) => a with 
            { 
                Capacity = a.Capacity + b.Capacity, 
                Durability = a.Durability + b.Durability, 
                Flavor = a.Flavor + b.Flavor, 
                Texture = a.Texture + b.Texture, 
                Calories = a.Calories + b.Calories 
            });

    private static IList<IList<int>> GenerateCombinationsOfTeaspoons(int count) 
        => GenericNumbersExtensions.GenericNumbers(101, count, c => c.Sum() == 100);

    private static List<Ingredient> ParseInput(string input)
        => input.Split('\n').Select(l => l.Trim())
            .Select(l => InputParseRegex().Match(l)
                .Map(match => new Ingredient(
                    Name: match.Groups["name"].Value,
                    Capacity: match.Groups["capacity"].Value.ToInt(),
                    Durability: match.Groups["durability"].Value.ToInt(),
                    Flavor: match.Groups["flavor"].Value.ToInt(),
                    Texture: match.Groups["texture"].Value.ToInt(),
                    Calories: match.Groups["calories"].Value.ToInt())))
            .ToList();
    
    [GeneratedRegex("(?<name>[A-Za-z]+): capacity (?<capacity>-?[0-9]+), durability (?<durability>-?[0-9]+), flavor (?<flavor>-?[0-9]+), texture (?<texture>-?[0-9]+), calories (?<calories>-?[0-9]+)")]
    private static partial Regex InputParseRegex();
}
