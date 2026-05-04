using UnityEngine;

[CreateAssetMenu(fileName = "NewFlatStat", menuName = "Stats/Flat")]
public class FlatStat : CharacterStat
{
    public override string GetDisplayText(float value)
    {

        return $"{GetDisplayValue(value)} additional {ID}";
    }

    public override string GetDisplayValue(float value)
    {
        var roundValue = System.Math.Round(value, roundingFactor);
        return $"{roundValue}";
    }
}