using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlots : MonoBehaviour, IDropHandler
{
    InventoryItemBehaviour currentItems;

    public InventoryItemBehaviour CurrentItems { get => currentItems;}

    private void Start()
    {
        currentItems = GetComponentInChildren<InventoryItemBehaviour>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItemBehaviour droppedItem = eventData.pointerDrag.GetComponent<InventoryItemBehaviour>();

        if (currentItems == null || ( droppedItem.ItemData == currentItems.ItemData && !currentItems.ItemMaxed && (droppedItem.ItemsAmount + currentItems.ItemsAmount) < 5))
        {
            Debug.LogWarning("Item Succesfully Placed On " + gameObject.name);

            AddItem(droppedItem);
            return;
        }

        Debug.LogWarning("Slot full: " + gameObject.name);
        return;
    }

    public void RemoveCurrentItem()
    {
        currentItems = null;
    }

    public void AddItem(InventoryItemBehaviour newItem)
    {
        if (currentItems == null)
        {
            newItem.OnItemDropped(gameObject.transform);
            currentItems = newItem;
            return;
        }

        currentItems.AddItem(newItem.ItemsAmount);
        Destroy(newItem.gameObject);
    }
}
