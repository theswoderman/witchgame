using UnityEngine;

[CreateAssetMenu(fileName = "ExpTable", menuName = "Scriptable Objects/ExpTable")]
public class ExpTable : ScriptableObject
{
    public int[] levels = new int[999];
    [Header("Generator Settings")]
    public int baseExp = 10;
    public float exponent = 1.2f;

    [ContextMenu("Generate Default EXP Values")]
    public void GenerateValues()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int level = i + 1;

            float calculatedExp = baseExp * Mathf.Pow(level, exponent);

            levels[i] = Mathf.RoundToInt(calculatedExp);
        }
    }

    public int GetRequireExp(int level)
    {
        //subtract 1 from level to get the index because indexes start at 0
        int index = level - 1;

        if (index >= levels.Length)
        {
            return int.MaxValue; //return the integer max if we have reached max level
        }

        return levels[index]; //return the assigned exp value for that level
    }
}
