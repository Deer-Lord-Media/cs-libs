namespace Ticker;

// can we get a timer from Unity?
record Container(HashSet<Ticker> Tickers)
{
    public void Tick()
    {
        // set DateTime.Now to start of tick
        foreach (Ticker ticker in this.Tickers) ticker.PreTick();
        // set DateTime.Now to end of tick
        foreach (Ticker ticker in this.Tickers) ticker.PostTick();
    }

    public void Register(Ticker ticker)
    {
        this.Tickers.Add(ticker);
    }

    public void Remove(Ticker ticker)
    {
        this.Tickers.Remove(ticker);
    }
}
