using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public string label;
    public TotemSlot[,] myTotems = new TotemSlot[10,10];
    public InventoryCell[,] inventoryCells = new InventoryCell[10, 10];

    private void Awake()
    {
        Dictionary<string, Inventory> inventories = SaveManager.Instance.inventories;
        if (inventories.TryGetValue(label, out Inventory value))
        {
            return;
        }
        inventories.Add(label, this);
    }

    //public void LoadTotem(Vector2Int origin, TotemData totem, bool skipValidation = false)
    //{
    //    if (skipValidation || IsPlacementValid(origin,totem))
    //    {
    //        TotemSlot newSlot = new TotemSlot();
    //        newSlot.totem = totem;
    //        Add(origin, newSlot);
    //    }
    //}

    private bool IsPlacementValid(Vector2Int origin, TotemData totem)
    {
        List<Vector2Int> totemOffsets = totem.totemOffsets;
        int gridRows = myTotems.GetLength(0);
        int gridCols = myTotems.GetLength(1);
        for (int i = 0; i < totem.totemOffsets.Count; i++)
        {
            Vector2Int targetCell = origin + totemOffsets[i];
            if (targetCell.x < 0 || targetCell.x >= gridRows || targetCell.y < 0 || targetCell.y >= gridCols)
            {
                return false;
            }
            if (myTotems[targetCell.x, targetCell.y] != null)
            {
                return false;
            }
        }
        return true;
    }

    public void LoadTotem(Vector2Int origin, TotemSlot newSlot, bool skipValidation = false)
    {
        if (skipValidation || IsPlacementValid(origin, newSlot.totem))
        {
            List<Vector2Int> offsets = newSlot.totem.totemOffsets;
            newSlot.origin = origin;
            for (int i = 0; i < offsets.Count; i++)
            {
                Vector2Int targetCell = origin + offsets[i];
                myTotems[targetCell.x, targetCell.y] = newSlot;
                inventoryCells[targetCell.x, targetCell.y].UpdateVisual();
            }
        }
    }

    private bool IsRemoveValid(Vector2Int coordinates)
    {
        return myTotems[coordinates.x, coordinates.y] != null;
    }

    public void Remove(Vector2Int coordinates)
    {
        if (IsRemoveValid(coordinates))
        {
            Vector2Int origin = myTotems[coordinates.x, coordinates.y].origin;
            TotemData totem = myTotems[coordinates.x, coordinates.y].totem;
            List<Vector2Int> totemOffsets = totem.totemOffsets;
            for (int i = 0; i < totemOffsets.Count; i++)
            {
                Vector2Int targetCell = origin + totemOffsets[i];
                myTotems[targetCell.x, targetCell.y] = null;
                inventoryCells[targetCell.x, targetCell.y].UpdateVisual();
            }
        }
    }
}
