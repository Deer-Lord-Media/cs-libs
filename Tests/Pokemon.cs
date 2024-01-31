namespace Tests;
using monsters;

public class PokemonTest
{
    [Fact]
    public void TestAttack()
    {
        var player = new monsters.Pokemon();
        var opponent = new monsters.Pokemon();
        var damage = player.UseMove(0, opponent);
        Assert.Equal(16, damage);
    }
}
