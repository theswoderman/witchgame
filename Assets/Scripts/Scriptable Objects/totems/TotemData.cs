using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "TotemData", menuName = "Inventory/Totem")]
public class TotemData : ScriptableObject
{
    public string totemID;
    public Sprite icon;
    //public StatInstance stat;
    public Row[] totem;
    public List<Vector2Int> totemOffsets;
    public Row[] aura;
    public List<Vector2Int> auraOffsets;
    public int gridSize = 9;//needs to be odd
    public float auraEffect;

    void Reset()
    {
        int centerPoint = CenterPoint();
        totem = new Row[gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            totem[i] = new Row();
            totem[i].cell = new bool[gridSize];
            if (i == centerPoint)
            {
                totem[centerPoint].cell[centerPoint] = true;
            }
        }
        aura = new Row[gridSize];
        for (int i = 0; i < gridSize; i++)
        {
            aura[i] = new Row();
            aura[i].cell = new bool[gridSize];
        }
    }

    private int CenterPoint()
    {
        return (gridSize - 1) / 2;
    }

    void OnEnable()
    {
        ConvertShapes();
    }

    public void ConvertShapes() //update to convert offsets relative to 4/4
    {
        totemOffsets.Clear();
        ConvertShape(totem, totemOffsets);
        auraOffsets.Clear();
        ConvertShape(aura, auraOffsets);
    }

    void ConvertShape(Row[] shape, List<Vector2Int> offsets)
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (shape[i].cell[j])
                {
                    Vector2Int offset = new Vector2Int(i-CenterPoint(), j- CenterPoint());
                    offsets.Add(offset);
                }
            }
        }
    }
}   

[System.Serializable]
public class Row
{
    public bool[] cell;
}