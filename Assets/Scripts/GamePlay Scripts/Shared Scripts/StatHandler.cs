using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class StatHandler : MonoBehaviour
{
    public Character character;
    public List<StatInstance> startingStats;
    [Serialize]
    public Dictionary<Statistic, StatInstance> statLookup = new Dictionary<Statistic, StatInstance>();
    public Dictionary<Statistic, StatInstance> statsChosen = new Dictionary<Statistic, StatInstance>();

    void Awake()
    {
        GenerateStatDictionary(statLookup, startingStats);
        character = GetComponent<Character>();
    }

    public void GenerateStatDictionary(Dictionary<Statistic, StatInstance> dictionary, List<StatInstance> list)
    {
        foreach (var stat in list)
        {
            dictionary.Add(stat.statistic, stat);
        }
    }

    public void GrowStat(Statistic stat, float statValue)
    {
        statLookup[stat].currentValue += statValue;
        if (stat == Statistic.increasedHealth || stat == Statistic.addedHealth || stat == Statistic.healthRegen)
        {
            character.characterHealth.UpdateHealth(character);
        }
    }

    public void UpdateStat(Statistic stat, float statValue)
    {
        if (character.isPlayer)
        {
            statLookup[stat].currentValue -= statsChosen[stat].currentValue;
            statsChosen[stat].currentValue = statValue;
            statLookup[stat].currentValue += statsChosen[stat].currentValue;

        }
        else
        {
            statLookup[stat].currentValue += statValue;
        }
        if (stat == Statistic.increasedHealth || stat == Statistic.addedHealth || stat == Statistic.healthRegen)
        {
            character.characterHealth.UpdateHealth(character);
        }
    }

    public float GetStat(Dictionary<Statistic,StatInstance> dictionary,Statistic stat)
    {
        return dictionary[stat].currentValue;
    }
}