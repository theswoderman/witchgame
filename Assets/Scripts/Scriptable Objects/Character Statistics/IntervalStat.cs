using UnityEngine;

[CreateAssetMenu(fileName = "NewIntervalStat", menuName = "Stats/Interval")]
public class IntervalStat : CharacterStat
{
    public override string GetDisplayText(float value)
    {
        return $"{ID} decreased by {GetDisplayValue(value)}";
    }

    public override string GetDisplayValue(float value)
    {
        if (value == 1)
        {
            return $"{value} second";
        }
        else
        {
            return $"{value} seconds";
        }
    }
}