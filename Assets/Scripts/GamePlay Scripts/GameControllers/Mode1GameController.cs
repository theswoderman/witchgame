using UnityEngine;
using System.Collections.Generic;

public class Mode1GameController : GameController
{
    public int experiencePoints;
    public ExpTable expTable;
    public Canvas choiceCanvas;
    public StatChoiceManager statChoiceManager;

    public override void OnEnemyDeath(Enemy enemy, Character killer)
    {
        base.OnEnemyDeath(enemy, killer);
        if (!killer.isPlayer) { return; }
        experiencePoints += enemy.expGranted;
        while (experiencePoints >= expTable.GetRequireExp(level))
        {
            LevelUp(player);
        }
    }

    public override void LevelUp(PlayerController player)
    {
        LevelUpEvent();
        level++;
        levelUpRewardsRemaining++;
        Pause();
    }

    public override void PrepareSave()
    {
        SaveManager.Instance.currentSaveData.chosenStats = new Dictionary<Statistic, StatInstance>(player.stats.statsChosen);
        base.PrepareSave();
    }
}
