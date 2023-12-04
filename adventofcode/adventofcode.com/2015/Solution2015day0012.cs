
using System.Dynamic;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace adventofcode.adventofcode.com._2015;

public partial class Solution2015day0012
{
    public class Environment
    {
        public List<JsonElement> List { get; set; } = new List<JsonElement>();
        public int Sum { get; set; }
    }
    
    public static int SolvePart1(string input)
        => MatchNumberRegex().Matches(input)
            .Select(m => int.Parse(m.Value))
            .Aggregate((a, b) => a + b);

    public static int SolvePart2(string input)
        => JsonSerializer.Deserialize<ExpandoObject>(input)
            .Map(json => json
                .Select(c => c.Value as JsonElement?)
                .Where(obj => obj.HasValue)
                .Select(obj => obj.Value)
                .ToList())
            .Map(list => new Environment() { List = list })
            .Map(env => Enumerable.Range(0, Int32.MaxValue)
                .TakeWhile(idx =>
                {
                    var node = env.List[idx];
                    env.Sum += node.ValueKind == JsonValueKind.Number ? node.GetInt32() : 0;
                    env.List.AddRange(node.ValueKind == JsonValueKind.Array 
                        ? node.EnumerateArray().AsEnumerable() 
                        : Array.Empty<JsonElement>().ToList());
                    env.List.AddRange(node.ValueKind == JsonValueKind.Object &&
                                      !node
                                          .EnumerateObject()
                                          .AsEnumerable()
                                          .Any(c => c.Value.ValueKind == JsonValueKind.String &&
                                                    c.Value.GetString() == "red")
                        ? node.EnumerateObject().AsEnumerable().Select(p => p.Value)
                        : Array.Empty<JsonElement>().ToList());
                    return idx < env.List.Count - 1;
                })
                .Aggregate((a, b) => b)
                .Map(_ => env.Sum));
    
    [GeneratedRegex("-?[0-9]+")]
    private static partial Regex MatchNumberRegex();
}
