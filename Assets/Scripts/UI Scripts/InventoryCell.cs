using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    public Vector2Int coordinates;
    public Inventory inventory;
    public GameController gameController;
    public Image displayImage;

    void Awake()
    {
        displayImage = GetComponent<Image>();
    }

    void Start()
    {
        gameController = GameController.Instance;
    }

    private bool emptyCell(Vector2Int coordinates)
    {
        return inventory.myTotems[coordinates.x,coordinates.y] == null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TotemSlot heldTotem = gameController.heldTotem;
        bool holdingTotem = heldTotem != null;
        if (!holdingTotem && emptyCell(coordinates))
        {
            return;
        }
        else if (!holdingTotem && !emptyCell(coordinates))
        {
            PickUp(inventory.myTotems[coordinates.x, coordinates.y]);
            inventory.Remove(coordinates);
        }
        else
        {
            Vector2Int pickUpCell = new Vector2Int(-1, -1);
            HashSet<TotemSlot> conflictingTotems = new HashSet<TotemSlot>();
            for (int i = 0; i < heldTotem.totem.totemOffsets.Count; i++)
            {
                Vector2Int targetCell = heldTotem.totem.totemOffsets[i] + coordinates;
                if (!emptyCell(targetCell))
                {
                    if (pickUpCell.x < 0)
                    {
                        pickUpCell = targetCell;
                    }
                    conflictingTotems.Add(inventory.myTotems[targetCell.x, targetCell.y]);
                }
            }
            int conflictCount = conflictingTotems.Count;
            if (conflictCount >= 2)
            {
                return;
            }
            else if (conflictCount == 0)
            {
                inventory.LoadTotem(coordinates, heldTotem, true);
                gameController.heldTotem = null;
            }
            else
            {
                PickUp(inventory.myTotems[pickUpCell.x,pickUpCell.y]);
                inventory.Remove(pickUpCell);
                inventory.LoadTotem(coordinates, heldTotem, true);
            }
        }
    }

    private void PickUp(TotemSlot totem)
    {
        gameController.heldTotem = totem;
    }

    public void UpdateVisual()
    {
        //Debug.Log("update visual called", this);
        if (!emptyCell(coordinates))
        {
            displayImage.color = Color.black;
        }
        else
        {
            displayImage.color = Color.white;
        }
    }
}