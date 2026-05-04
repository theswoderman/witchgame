using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveSlot
{
    public string inventoryLabel;
    public string totemID;
    public Vector2Int origin;
    public StatInstance stat;
}

[System.Serializable]
public class SaveData : ISerializationCallbackReceiver
{
    #region chosenStats Dictionary
    [System.NonSerialized] public Dictionary<Statistic, StatInstance> chosenStats = new Dictionary<Statistic, StatInstance>();

    [SerializeField] private List<Statistic> chosenStatsKeys = new List<Statistic>();
    [SerializeField] private List<StatInstance> chosenStatsValues = new List<StatInstance>();
    #endregion
    //TODO: add more things things to save here
    public List<SaveSlot> savedInventory = new List<SaveSlot>();
    public string characterId;


    #region Dictionary Managers
    public void OnBeforeSerialize()
    {
        SyncToLists(chosenStats, chosenStatsKeys, chosenStatsValues);
        //Add additional dictionaries here inside SyncToLists() like above
    }

    public void OnAfterDeserialize()
    {
        SyncToDictionary(chosenStats, chosenStatsKeys, chosenStatsValues);
        //Add additional dictionaries here inside SyncToDictionary() like above
    }
    #endregion

    #region Helper Functions
    private void SyncToLists<TKey,TValue>(Dictionary<TKey, TValue> dict, List<TKey> keys, List<TValue> values)
    {
        keys.Clear();
        values.Clear();
        foreach (var pair in dict)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    private void SyncToDictionary<TKey, TValue>(Dictionary<TKey, TValue> dict, List<TKey> keys, List<TValue> values)
    {
        dict.Clear();
        // Basic safety check
        if (keys.Count != values.Count) return;

        for (int i = 0; i < keys.Count; i++)
        {
            dict.Add(keys[i], values[i]);
        }
    }
#endregion
}
