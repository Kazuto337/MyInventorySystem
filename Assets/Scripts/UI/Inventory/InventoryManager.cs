using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    List<InventorySlots> inventorySlots;

    public void AddItem2Inventory(InventoryItemBehaviour newItem)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            bool ConditionB = inventorySlots[i].CurrentItems.ItemData == newItem.ItemData && !inventorySlots[i].CurrentItems.ItemMaxed;
            if (inventorySlots[i].CurrentItems != null ||  ConditionB)
            {
                inventorySlots[i].AddItem(newItem);
            }
        }
    }
}
