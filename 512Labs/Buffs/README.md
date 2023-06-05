# USING THIS LIBRARY
## Example Classes 

```csharp
// mandatory
record YourBuff : Data
{
    // additional attributes and methods
}

// extra
record YourDebuff : Data
{
    // additional attributes and methods
}
```

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
The specifics of how buff/debuffs work together is up to your code; this library simply provides the tracker and data mechanisms. If you want to track and calculate both applied Buffs and Debuffs, you might consider the following

```csharp
enum BuffType
{
    Buff,
    Debuff
}
record YourBuff : Data
{
    public BuffType type;  // 0 for buff, 1 for debuff
}

var buffs = Buffs.Collection<YourBuff>();

void CalculateBuffedStats(Buffs.Collection<YourBuff> buffs)
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
