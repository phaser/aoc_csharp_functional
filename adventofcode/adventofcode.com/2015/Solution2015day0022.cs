
namespace adventofcode.adventofcode.com._2015;

public static class Solution2015day0022
{
    public record Effect(int Turns, int Armor, int Damage, int Mana, int Heal);

    private record Spell(int ManaCost, int Damage, int Heal, Effect? Effect = null);

    public record Player(int HitPoints, int Mana = 0, int Damage = 0, int Armor = 0)
    {
        public List<Effect> Effects { get; init; } = new List<Effect>();
    }
    
    private static Spell[] Spells = new Spell[] {
        new Spell(53, 4, 0),                                                            // 0. Magic Missile
        new Spell(73, 2, 2),                                                            // 1. Drain
        new Spell(113, 0, 0, new Effect(6, 7, 0, 0, 0)), // 2. Shield
        new Spell(173, 0, 0, new Effect(6, 0, 3, 0, 0)), // 3. Poison
        new Spell(229, 0, 0, new Effect(5, 0, 0, 101, 0))// 4. Recharge
    };

    public static (int Winner, int Mana) WhoWins(Player player, int[] spellsToCast, Player boss)
        => Enumerable.Range(0, Math.Max(player.HitPoints, boss.HitPoints))
            .TakeWhile(round =>
            {
                (player, boss) = round % 2 == 0 ? PlayerMove(round, player, spellsToCast, boss) : BossMove(player, boss);
                return player.HitPoints > 0 && boss.HitPoints > 0;
            }).ToList()
            .Map(rounds =>
            (
                Winner: player.HitPoints > 0 ? 0 : 1,
                Mana: rounds.Append(rounds.Last() + 1)
                    .Select(idx => idx < spellsToCast.Length ? Spells[spellsToCast[idx]].ManaCost : 0).Sum()
            ));

    private static (Player player, Player boss) BossMove(Player player, Player boss)
    {
        var armorEffect = boss.Effects.FirstOrDefault(effect => effect.Armor != 0);
        boss = boss with
        {
            Effects = boss.Effects.Select(effect => effect with { Turns = effect.Turns - 1 })
                .Where(effect => effect.Turns >= 0).ToList()
        };
        var stillHasArmorEffect = boss.Effects.Any(effect => effect.Armor != 0);
        if (armorEffect != null && !stillHasArmorEffect)
        {
            player = player with { Armor = player.Armor - armorEffect.Armor };
        }
        var effect = boss.Effects.Count > 0 ? boss.Effects.Aggregate((a, b) => a with { Damage = a.Damage + b.Damage, Heal = a.Heal + b.Heal }) : new Effect(0, 0, 0, 0, 0);
        boss = boss with { HitPoints = boss.HitPoints - effect.Damage };
        if (boss.HitPoints <= 0)
            return (player, boss);
        
        var damage = boss.Damage - player.Armor;
        player = player with
        {
            HitPoints = player.HitPoints + effect.Heal - (damage <= 0 ? 1 : damage),
            Mana = player.Mana + effect.Mana
        };
        return (player, boss);
    }

    private static (Player player, Player boss) PlayerMove(int round, Player player, int[] spellsToCast, Player boss)
    {
        var armorEffect = boss.Effects.FirstOrDefault(effect => effect.Armor != 0);
        boss = boss with
        {
            Effects = boss.Effects.Select(effect => effect with { Turns = effect.Turns - 1 })
                .Where(effect => effect.Turns >= 0).ToList()
        };
        var stillHasArmorEffect = boss.Effects.Any(effect => effect.Armor != 0);
        if (armorEffect != null && !stillHasArmorEffect)
        {
            player = player with { Armor = player.Armor - armorEffect.Armor };
        }
        var effect = boss.Effects.Count > 0 ? boss.Effects.Aggregate((a, b) => a with { Damage = a.Damage + b.Damage, Heal = a.Heal + b.Heal }) : new Effect(0, 0, 0, 0, 0);
        var spellIndex = round / 2;
        var currentSpell = spellIndex < spellsToCast.Length ? spellsToCast[spellIndex] : -1;
        if (currentSpell == -1)
            throw new ArgumentException("consumed all spells");
        player = player with
        {
            HitPoints = player.HitPoints + Spells[currentSpell].Heal + effect.Heal,
            Mana = player.Mana + effect.Mana
        };
        if (Spells[currentSpell].ManaCost > player.Mana)
            return (player with
            {
                HitPoints = -1
            }, boss);
        boss = boss with { HitPoints = boss.HitPoints - Spells[currentSpell].Damage - effect.Damage };
        if (Spells[currentSpell].Effect == null) return (player, boss);
        if (currentSpell == 2)
        {
            player = player with { Armor = Spells[currentSpell].Effect.Armor };
        }
        boss.Effects.Add(Spells[currentSpell].Effect!);
        return (player, boss);
    }
}
