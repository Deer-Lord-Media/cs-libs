namespace Ticker;

record Ticker(List<Action> preTicks, List<Action> postTicks)
{
    private List<Action> preTicks = preTicks;
    private List<Action> postTicks = postTicks;

    public void PreTick()
    {
        foreach (Action preTick in this.preTicks) preTick();
    }

    public void PostTick()
    {
        foreach (Action postTick in this.postTicks) postTick();
    }
}
