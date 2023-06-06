namespace Ticker;

record Collection
{
    private HashSet<Ticker> tickers = new HashSet<Ticker>();

    public void Tick()
    {
        foreach (Ticker ticker in this.tickers) ticker.PreTick();
        // update datetime
        foreach (Ticker ticker in this.tickers) ticker.PostTick();        
    }

    public void Add(Ticker ticker)
    {
        this.tickers.Add(ticker);
    }

    public void Remove(Ticker ticker)
    {
        this.tickers.Remove(ticker);
    }
}
