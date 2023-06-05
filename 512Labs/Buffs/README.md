# USING THIS LIBRARY

## Buffs
- Behavior dictated via Buffs.Apply() method
- Describes the buff to be applied
- Can add more attributes/methods to expand for your data
- similarTo
    defines other Buff Ids that are mutually exclusive and prevent this buff from applying
- precendenceOver
    defines other Buff Ids that should absolutely be pushed off when this buff is applied
    this would include something like a higher level version of the same buff

## Debuffs
- Behavior dictated via Buffs.Debuff() method
- Describes a debuff, which works to *remove* a specific set of buffs
- Can add more attributes/methods to expand for your data
- similarTo
    unused at the moment
- precedenceOver
    defines which Buffs this debuff cancels

## Notes
The specifics of how buff/debuffs work together and are handled is up to your code; this library simply provides the tracker and basic data mechanisms. This example provides one collection to track both buffs and debuffs.

```csharp
enum BuffType
{
    Buff,
    Debuff
}

// extend the Data class as necessary for your implementation
record YourBuff : Buffs.Data
{
    public BuffType type;  // 0 for buff, 1 for debuff
}

var buffs = Buffs.Collection<YourBuff>();

void HandleBuffS(Buffs.Collection<YourBuff> buffs)
{
    int stats = 0;
    foreach (Buff<YourBuff> buff in buffs)
    {

        switch (buff.type)
        {
            case BuffType.Buff:
                stat += buff.Data.YourStatData;
                break;
            case BuffType.Debuff:
                stat -= buff.Data.YourStatData;
                break;
            default:
                //
                break;
        }
    }
}
```
