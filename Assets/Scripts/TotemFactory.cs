using UnityEngine;

public class TotemFactory
{
    private TotemDatabase database;

    public TotemFactory(TotemDatabase database)
    {
        this.database = database;
    }

    //in the future if I want to have multiple databases, I could have the database be a parameter in the function instead of determined by the contructor
    public TotemSlot CreateTotem(StatInstance stat)
    {
        int random = Random.Range(0, database.allTotems.Count);
        TotemData totem = database.allTotems[random];
        TotemSlot slot = new TotemSlot();
        slot.totem = totem;
        slot.stat = stat;
        return slot;
    }
}