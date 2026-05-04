using UnityEngine;

public enum Statistic
{
    damageInterval,
    flatDamage,
    increasedDamage,
    growthSpeed,
    healthRegen,
    addedHealth,
    increasedHealth,
    movementSpeed,
    size
}

public class CharacterStat : ScriptableObject
{
    public Statistic ID;
    public float startingValue;
    public Sprite icon;
    public AnimationClip animationClip;
    public float minGrantedValue;
    public float maxGrantedValue;
    public int roundingFactor;

    public virtual string GetDisplayText(float value)
    {
        return $"{ID}: {value}";
    }

    public virtual string GetDisplayValue(float value)
    {
        return $"{ID}: {value}";
    }
}

[System.Serializable]
public class StatInstance
{
    public Statistic statistic;
    public float currentValue;

    public StatInstance(Statistic stat,float statValue)
    {
        this.statistic = stat;
        this.currentValue = statValue;
    }
}