
namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0022
{
    public record Effect(int Turns, int Armor, int Damage, int Mana, int Heal);

    private record Spell(int ManaCost, int Damage, int Heal, Effect? Effect = null);

    public record Player(int HitPoints, int Mana = 0, int Damage = 0)
    {
        public List<Effect> Effects { get; init; } = new List<Effect>();
    }
    
    private static Spell[] Spells = new Spell[] {
        new Spell(53, 4, 0),                                                            // 0. Magic Missle
        new Spell(73, 2, 2),                                                            // 1. Drain
        new Spell(113, 0, 0, new Effect(6, 7, 0, 0, 0)), // 2. Shield
        new Spell(173, 0, 0, new Effect(6, 0, 3, 0, 0)), // 3. Poison
        new Spell(113, 0, 0, new Effect(5, 0, 0, 101, 0))// 4. Recharge
    };

    public static (int Winner, int Mana) WhoWins(Player player, int[] spellsToCast, Player boss)
        => Enumerable.Range(0, Math.Max(player.HitPoints, boss.HitPoints))
            .TakeWhile(round =>
            {
                (player, boss) = round % 2 == 0 ? PlayerMove(round, player, spellsToCast, boss) : BossMove(player, boss);
                return player.HitPoints > 0 && boss.HitPoints > 0;
            }).ToList()
            .And(rounds =>
            (
                Winner: player.HitPoints > 0 ? 0 : 1,
                Mana: rounds.Append(rounds.Last() + 1)
                    .Select(idx => idx < spellsToCast.Length ? Spells[spellsToCast[idx]].ManaCost : 0).Sum()
            ));

    private static (Player player, Player boss) BossMove(Player player, Player boss)
        => (player with {
            HitPoints = player.HitPoints - boss.Damage
        }, boss);

    private static (Player player, Player boss) PlayerMove(int round, Player player, int[] spellsToCast, Player boss)
    {
        throw new NotImplementedException();
    }
}
