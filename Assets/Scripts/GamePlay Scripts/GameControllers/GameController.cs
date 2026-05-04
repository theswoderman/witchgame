using System;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    public bool isPaused;
    public static event Action OnLevelUp;
    public int level = 1;
    public int levelUpRewardsRemaining = 0;
    public static GameController Instance;
    public TotemSlot heldTotem;
    public SaveManager saveManager;
    public List<CharacterStat> statList; //If character is growing infinitely it's probably a problem with this

    void Awake()
    {
        Instance = this;
    }

    public virtual void Start()
    {
        saveManager = SaveManager.Instance;
        if (!string.IsNullOrEmpty(saveManager.currentSaveData.characterId))
        {
            GameObject playerCharacter = saveManager.characterDB.GetCharacterbById(saveManager.currentSaveData.characterId).prefab;
            Instantiate(playerCharacter, Vector3.zero, Quaternion.identity);
            player = PlayerController.Instance;
            player.sendNewTargetToCamera();
        }
        //TODO: remove this, only exists for testing
        TotemFactory factory = new TotemFactory(saveManager.totemDB);
        heldTotem = factory.CreateTotem(new StatInstance(Statistic.addedHealth,1));
    }

    public virtual void OnEnemyDeath(Enemy enemy, Character killer)
    {
    }
    
    public virtual void PrepareSave()
    {
        //TODO: generate SaveDate 
        //TODO: iterate over stash as well
        HashSet<TotemSlot> myTotems = new HashSet<TotemSlot>();
        TotemSlot[,] inventoryGrid = player.inventory.myTotems;
        for (int i = 0; i < inventoryGrid.GetLength(0); i++)
        {
            for (int j = 0; j < inventoryGrid.GetLength(1); j++)
            {
                if (inventoryGrid[i, j] != null)
                {
                    myTotems.Add(inventoryGrid[i, j]);
                }
            }
        }
        saveManager.currentSaveData.savedInventory = new List<SaveSlot>();
        foreach (var totem in myTotems)
        {
            SaveSlot savedTotem = new SaveSlot();
            savedTotem.stat = totem.stat;
            savedTotem.origin = totem.origin;
            savedTotem.totemID = totem.totem.totemID;
            savedTotem.inventoryLabel = player.inventory.label;
            saveManager.currentSaveData.savedInventory.Add(savedTotem);
        }
        saveManager.currentSaveData.characterId = player.characterData.characterID;
        saveManager.Save();
    }

    protected void LevelUpEvent()
    {
        OnLevelUp?.Invoke();
    }

    public virtual void LevelUp(PlayerController player)
    {

    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void ToggleGameObject(GameObject gameObject, bool skip=false)
    {
        if (skip == true) { return; }
        if (gameObject == null) { return; }
        bool currentStatus = gameObject.activeSelf;
        gameObject.SetActive(!currentStatus);
    }

    public CharacterStat GetDefinition(Statistic stat)
    {
        return statList.Find(d => d.ID == stat);
    }
}
