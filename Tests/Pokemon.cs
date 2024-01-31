namespace Tests;
using monsters;

public class PokemonTest
{
    [Fact]
    public void TestAttack()
    {
        Move[] moves = new Move[4];
        moves[0] = new Move(40, 100, Element.Fire, StatName.Attack);

        BaseStats baseStats = new BaseStats(20, 10, 12, 18, 22);
        BaseStats ivs = new BaseStats(3, 3, 3, 3, 3);
        BaseStats evs = new BaseStats(0, 0, 0, 0, 0);
        MonsterStats permanentStats = new MonsterStats(5, baseStats, ivs, evs);
        var player = new monsters.Pokemon(permanentStats, Element.Normal, moves: moves);
        var opponent = new monsters.Pokemon(permanentStats, Element.Normal, moves: moves);
        Assert.Equal(18, opponent.Health);
        var damage = player.UseMove(0, opponent);
        Assert.Equal(6, damage);
        Assert.Equal(12, opponent.Health);
    }

    [Fact]
    public void TestStats()
    {
        BaseStats baseStats = new BaseStats(20, 10, 12, 18, 22);
        BaseStats ivs = new BaseStats(3, 3, 3, 3, 3);
        BaseStats evs = new BaseStats(1, 1, 1, 1, 1);
        MonsterStats monsterStats = new MonsterStats(5, baseStats, ivs, evs);
        Assert.Equal(18, monsterStats.Health);
        Assert.Equal(7, monsterStats.Attack);
        Assert.Equal(7, monsterStats.Defense);
        Assert.Equal(8, monsterStats.Speed);
        Assert.Equal(8, monsterStats.Special);
        Assert.Equal(8, monsterStats.SpecialAttack);
        Assert.Equal(8, monsterStats.SpecialDefense);
    }
}
