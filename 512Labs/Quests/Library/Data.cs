using System;
namespace Quests;

public abstract class Data : IEquatable<Data>
{

    public int Id;
    public string Name = "";
    public string Description = "";
    public int TotalStages { get; }

    public bool Equals(Data? obj)
    {
        return (obj != null) && this.Id.Equals(obj.Id);
    }
}
