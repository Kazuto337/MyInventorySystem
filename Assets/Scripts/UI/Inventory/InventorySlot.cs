using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] InventoryItemBehaviour currentItems;

    public InventoryItemBehaviour CurrentItems { get => currentItems; }

    private void Awake()
    {
        currentItems = GetComponentInChildren<InventoryItemBehaviour>();
    }
    public virtual void OnDrop(PointerEventData eventData)
    {
        InventoryItemBehaviour droppedItem = eventData.pointerDrag.GetComponent<InventoryItemBehaviour>();

        if (currentItems == null)
        {
            Debug.LogWarning("Item Succesfully Placed On " + gameObject.name);

            AddItem(droppedItem);
            return;
        }
        else if (droppedItem.ItemData != currentItems.ItemData)
        {
            Debug.LogWarning("Slot full: " + gameObject.name);
            return;

        }

        if (currentItems is ConsumableItem )
        {
            if ((currentItems as ConsumableItem).ItemMaxed)
            {
                Debug.LogWarning("Slot full: " + gameObject.name);
                return;
            }

            if ((droppedItem as ConsumableItem).ItemsAmount + (currentItems as ConsumableItem).ItemsAmount <= 5)
            {
                Debug.LogWarning("Item Succesfully Placed On " + gameObject.name);

                AddItem(droppedItem);
                return;
            }
        }
        Debug.LogWarning("Slot full: " + gameObject.name);
        return;
    }

    public void RemoveItem()
    {
        if ((currentItems is ConsumableItem) == false)
        {
            return;
        }

        if ((currentItems as ConsumableItem).ItemsAmount > 1)
        {
            (currentItems as ConsumableItem).RemoveItem();
            return;
        }

        DeleteCurrentItem();
    }

    public void DeleteCurrentItem()
    {
        currentItems = null;
    }

    public virtual void AddItem(InventoryItemBehaviour newItem)
    {
        if (currentItems == null)
        {
            newItem.OnItemDropped(gameObject.transform);
            currentItems = newItem;
            return;
        }

        if (currentItems is ConsumableItem && newItem is ConsumableItem)
        {
            (currentItems as ConsumableItem).AddItem((newItem as ConsumableItem).ItemsAmount);
            Destroy(newItem.gameObject);
        }

    }
}
