using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public TotemDatabase totemDB;
    public CharacterDatabase characterDB;
    public SaveData currentSaveData = new SaveData();
    private string savePath;
    public Dictionary<string, Inventory> inventories = new Dictionary<string, Inventory>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Application.persistentDataPath + "/savefile.json";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(currentSaveData, true);

        File.WriteAllText(savePath, json);

        Debug.Log($"Game saved to: {savePath}");
    }

    public void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            JsonUtility.FromJsonOverwrite(json, currentSaveData);

            foreach (var savedSlot in currentSaveData.savedInventory)
            {
                TotemSlot slot = new TotemSlot();
                slot.totem = totemDB.GetTotemByID(savedSlot.totemID);
                Vector2Int origin = savedSlot.origin;
                slot.stat = savedSlot.stat;
                inventories[savedSlot.inventoryLabel].LoadTotem(origin, slot, true);
            }
            Debug.Log($"Game loaded from: {savePath}");
        }
    }
}
