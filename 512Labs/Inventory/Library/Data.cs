namespace Inventory;

public abstract class Data : IEquatable<Data>
{
    public int Id;  
    public int MaxStacks;

    public bool Equals(Data? obj)
    {
        return (obj != null) && this.Id.Equals(obj.Id);
    }
}
