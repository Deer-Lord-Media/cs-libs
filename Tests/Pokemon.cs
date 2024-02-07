namespace Tests;
using Pokemon;

public class PokemonTest
{
    [Fact]
    public void TestStats()
    {
        Pokemon.Stats.Basic dataStats = new(35, 55, 40, 90, 50);
        Pokemon.Data.Model data = new(
            1,
            dataStats,
            dataStats,
            Pokemon.Data.Type.Fire
        );
        Pokemon.Stats.Basic baseStats = new(20, 10, 8, 12, 30);
        Pokemon.Stats.Basic ivs = new(3, 3, 3, 3, 3);
        Pokemon.Stats.Basic evs = new(5, 5, 5 ,5 ,5);
        Pokemon.Stats.Calculated stats = new(
            5,
            100,
            baseStats,
            ivs,
            evs
        );
        Pokemon.Monster pokemon = new(
            stats,
            data
        );
        Pokemon.Stats.Basic current = pokemon.Stats.Current;
        Assert.Equal(18, current.Health);
        Assert.Equal(7, current.Attack);
        Assert.Equal(7, current.Defense);
        Assert.Equal(7, current.Speed);
        Assert.Equal(9, current.Special);
        Assert.Equal(8, current.SpcAttack);
        Assert.Equal(8, current.SpcDefense);
    }

    // [Fact]
    // public void TestDamage()
    // {
    //     Pokemon.MonsterData data = new MonsterData(Pokemon.Type.Fire, Pokemon.Type.Dragon);
    //     Pokemon.BasicStats baseStats = new Pokemon.BasicStats(20, 10, 8, 12, 30);
    //     Pokemon.BasicStats ivs = new Pokemon.BasicStats(3, 3, 3, 3, 3);
    //     Pokemon.BasicStats evs = new Pokemon.BasicStats(5, 5, 5 ,5 ,5);
    //     Pokemon.Ability ability = new Pokemon.Ability(40, 100, Pokemon.Type.Fire, Pokemon.StatName.Attack);
    //     Pokemon.Ability[] abilities = new Pokemon.Ability[]{
    //         ability
    //     };
    //     Pokemon.Monster pokemon = new Pokemon.Monster(
    //         5, 0, baseStats, ivs, evs, data, abilities
    //     );
    //     Assert.Equal(18, pokemon.CurrentHealth);
    //     int damage = pokemon.UseAbility(0, pokemon);
    //     Assert.Equal(3, damage);
    //     Assert.Equal(15, pokemon.CurrentHealth);
    // }
}
