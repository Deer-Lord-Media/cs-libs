## Basics of an encounter
state
- 0: don't know about wolf
- 1: know about wolf
event
- see the wolf!
outcome
- no wolf

not enough information for narrative

## Build it in Sorcery
Two Encounters

Peasant's House
- meet peasant, told about wolf; player.wolf_state.set(1)
    - player.wolf_state = 0; don't know
    - 1; know
    - 2; killed it
    - 3; give head
- revist peasant
    - player.wolf_state analyzed again; <2, peasant will leave
- peasant gone
    - player.wolf_state cannot update
    - peasant.impatient++; not a boolean, this is the progression

The woods
- see wolf
  - kill it
    - update player.wolf_state = 2
  - run away
- revisit wolf
    - player.wolf_state<2 && peasant.impatient > 2 == wolf asleep


## States capture complexity
Wolf
- know about wolf
- wolf seen
- wolf killed
- wolf's head cut off

peasant.wolf
- peasant met
- peasant told of wolf
- peasant happy
peasant.impatience
- none
- impatient
- gone
- found dead (tried to kill wolf)
