namespace Tests;
using Pokemon;

public class PokemonTest
{
    [Fact]
    public void TestStats()
    {
        Pokemon.MonsterData data = new MonsterData(Pokemon.Type.Fire, Pokemon.Type.Dragon);
        Pokemon.Stats baseStats = new Pokemon.Stats(20, 10, 8, 12, 30);
        Pokemon.Stats ivs = new Pokemon.Stats(3, 3, 3, 3, 3);
        Pokemon.Stats evs = new Pokemon.Stats(5, 5, 5 ,5 ,5);
        Pokemon.Monster pokemon = new Pokemon.Monster(
            5, 0, baseStats, ivs, evs, data
        );
        Stats stats = pokemon.Stats;
        Assert.Equal(18, stats.Health);
        Assert.Equal(7, stats.Attack);
        Assert.Equal(7, stats.Defense);
        Assert.Equal(7, stats.Speed);
        Assert.Equal(9, stats.Special);
        Assert.Equal(8, stats.SpcAttack);
        Assert.Equal(8, stats.SpcDefense);

        pokemon.BaseStats += baseStats;
        stats = pokemon.Stats;
        Assert.Equal(20, stats.Health);
        Assert.Equal(8, stats.Attack);
        Assert.Equal(7, stats.Defense);
        Assert.Equal(8, stats.Speed);
        Assert.Equal(12, stats.Special);
        Assert.Equal(10, stats.SpcAttack);
        Assert.Equal(9, stats.SpcDefense);
    }

    [Fact]
    public void TestDamage()
    {
        Pokemon.MonsterData data = new MonsterData(Pokemon.Type.Fire, Pokemon.Type.Dragon);
        Pokemon.Stats baseStats = new Pokemon.Stats(20, 10, 8, 12, 30);
        Pokemon.Stats ivs = new Pokemon.Stats(3, 3, 3, 3, 3);
        Pokemon.Stats evs = new Pokemon.Stats(5, 5, 5 ,5 ,5);
        Pokemon.Ability ability = new Pokemon.Ability(40, 100, Pokemon.Type.Fire, Pokemon.StatName.Attack);
        Pokemon.Ability[] abilities = new Pokemon.Ability[]{
            ability
        };
        Pokemon.Monster pokemon = new Pokemon.Monster(
            5, 0, baseStats, ivs, evs, data, abilities
        );
        int damage = pokemon.UseAbility(0, pokemon);
        Assert.Equal(3, damage);
    }
}
