using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TotemDatabase", menuName = "Inventory/TotemDatabase")]
public class TotemDatabase : ScriptableObject
{
    public List<TotemData> allTotems;

    public Dictionary<string, TotemData> totemLookup;

    public void Initialize()
    {
        totemLookup = new Dictionary<string, TotemData>();
        foreach (var totem in allTotems)
        {
            if (!totemLookup.ContainsKey(totem.totemID))
            {
                totemLookup.Add(totem.totemID, totem);
            }
        }
    }

    //returns a TotemData
    public TotemData GetTotemByID(string id)
    {
        if (totemLookup == null) Initialize();
        return totemLookup.TryGetValue(id, out TotemData totem) ? totem : null;
    }
}
