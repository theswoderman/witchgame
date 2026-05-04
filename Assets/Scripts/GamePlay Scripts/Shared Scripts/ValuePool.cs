using UnityEngine;

[System.Serializable]
public class ValuePool
{
    public float current;
    public float min = 0;
    public float max;
    public float regen; // value is per second

    // can be used to easily find the percentage full that a characters health is
    public float GetPercentage()
    {
        if (max <= 0) return 0;
        return current / max;
    }

    public void AdjustCurrent(float amount)
    {
        current += amount;
        current = Mathf.Clamp(current, min, max);
    }

    public void UpdateHealth(Character character)
    {
        float percent = GetPercentage(); // snapshot this
        max = character.stats.GetStat(character.stats.statLookup, Statistic.addedHealth) * ((character.stats.GetStat(character.stats.statLookup, Statistic.addedHealth) / 100) + 1);
        current = max * percent;
        regen = character.stats.GetStat(character.stats.statLookup, Statistic.healthRegen);
    }

    public void Update()
    {
        if (regen != 0 && current < max)
        {
            current += regen * Time.deltaTime;
        }
    }
}
