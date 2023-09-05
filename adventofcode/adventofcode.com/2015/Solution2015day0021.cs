
namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0021
{
    private record Item(int Cost, int Damage, int Armor)
    {
        public static Item operator +(Item a, Item b)
            => new Item(Cost: a.Cost + b.Cost, Armor: a.Armor + b.Armor, Damage: a.Damage + b.Damage);
    }
    
    public record Player(int HitPoints, int Damage, int Armor);
    
    private static List<Item> Weapons = new()
    {
        new(8, 4, 0),
        new(10, 5, 0),
        new(25, 6, 0),
        new(40, 7, 0),
        new(74, 8, 0)
    };

    private static List<Item> Armor = new()
    {
        new(13, 0, 1),
        new(31, 0, 2),
        new(53, 0, 3),
        new(75, 0, 4),
        new(102, 0, 5)
    };

    private static List<Item> Rings = new()
    {
        new(25, 1, 0),
        new(50, 2, 0),
        new(100, 3, 0),
        new(20, 0, 1),
        new(40, 0, 2),
        new(80, 0, 3),
    };

    public static int SolvePart1(Player boss)
        => GenerateAllValidCombinationsOfItems()
            .Select(ConstructCombinationOfStats)
            .Where(cfg => WhoWins(new Player(100, cfg.Damage, cfg.Armor), boss) == 0)
            .MinBy(match => match.Cost)!.Cost;

    public static int SolvePart2(Player boss)
        => GenerateAllValidCombinationsOfItems()
            .Select(ConstructCombinationOfStats)
            .Where(cfg => WhoWins(new Player(100, cfg.Damage, cfg.Armor), boss) == 1)
            .MaxBy(match => match.Cost)!.Cost;

    private static Item ConstructCombinationOfStats(IList<int> combo)
    {
        var zeroItem = new Item(0, 0, 0);
        var config =
            Weapons[combo[0] - 1] +
            (combo[1] != 0 ? Armor[combo[1] - 1] : zeroItem) +
            (combo[2] != 0 ? Rings[combo[2] - 1] : zeroItem) +
            (combo[3] != 0 ? Rings[combo[3] - 1] : zeroItem);
        return config;
    }

    private static IList<IList<int>> GenerateAllValidCombinationsOfItems() 
        => GenericNumbersExtensions.GenericNumbers(7, 4,
            configuration => 
                configuration[0] > 0 && 
                configuration[0] < 6 && 
                configuration[1] < 6 && 
                ((configuration[2] == 0 && configuration[3] == 0) || configuration[2] < configuration[3]));

    /// <summary>
    /// Determines who wins a game, starting with some stats
    /// </summary>
    /// <param name="player"></param>
    /// <param name="boss"></param>
    /// <returns>0 = player wins; 1 = boss wins</returns>
    /// <exception cref="NotImplementedException"></exception>
    public static int WhoWins(Player player, Player boss)
        => Enumerable.Range(0, Math.Max(player.HitPoints, boss.HitPoints))
            .TakeWhile(round =>
            {
                var attacker = round % 2 == 0 ? player : boss;
                var defender = round % 2 == 0 ? boss : player;
                var hits = attacker.Damage - defender.Armor;
                hits = hits <= 0 ? 1 : hits;
                defender = defender with { HitPoints = defender.HitPoints - hits };
                (player, boss) = round % 2 == 0 ? (attacker, defender) : (defender, attacker);
                return player.HitPoints > 0 && boss.HitPoints > 0;
            })
            .Aggregate((_, b) => b)
            .And(_ => player.HitPoints > 0 ? 0 : 1);
}
