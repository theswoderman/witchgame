using UnityEngine;

[CreateAssetMenu(fileName = "NewPercentStat", menuName = "Stats/Percent")]
public class PercentStat : CharacterStat
{
    public override string GetDisplayText(float value)
    {
        return $"{ID} increased by {GetDisplayValue(value)}";
    }
    public override string GetDisplayValue(float value)
    {
        return $"{value}%";
    }
}