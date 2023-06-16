# USING THIS LIBRARY

## Buffs
- Behavior dictated via Buffs.Apply() method
- [Buffs.Data](./Library/Data.cs) describes the buff to be applied

```csharp
var buffs = Buffs.Tracker(16);

void HandleBuff(Buffs.Buff buff)
{
    /*
        A simple foreign key on this Id
        is probably the easiest way to 
        associate this data with the
        rest of your buff-related data.
    */
    // buff.Data.Id;
}

foreach (Buffs.Buff buff in buffs.Active)
{
    HandleBuff(buff);
}
```
