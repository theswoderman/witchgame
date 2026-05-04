using UnityEngine;

public class GenerateInventoryGrid : MonoBehaviour
{
    public InventoryCell cellPrefab;
    public GameObject parent;
    public Inventory inventory;

    public void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        
        int rowCount = inventory.myTotems.GetLength(0);
        int columnCount = inventory.myTotems.GetLength(1);
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                InventoryCell newCell = Instantiate(cellPrefab, parent.transform);
                newCell.coordinates = new Vector2Int(i,j);
                newCell.inventory = inventory;
                inventory.inventoryCells[i, j] = newCell;
            }
        }
    }
}
