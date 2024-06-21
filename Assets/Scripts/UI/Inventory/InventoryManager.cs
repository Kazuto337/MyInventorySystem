using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public static InventoryManager Instance { get => instance; }

    [SerializeField] List<ClotheSlots> equippmentSlots;
    [SerializeField] List<InventorySlot> inventorySlots;

    public List<InventorySlot> Slots { get => inventorySlots;}
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void AddItem2Inventory(InventoryItemBehaviour newItem)
    {
        foreach (var slot in inventorySlots)
        {
            if (slot.CurrentItems == null)
            {
                slot.AddItem(newItem);
                break;
            }

            if (newItem is ConsumableItem)
            {
                if (slot.CurrentItems.ItemData == newItem.ItemData && !(slot.CurrentItems as ConsumableItem).ItemMaxed)
                {
                    slot.AddItem(newItem);
                    break;
                }
            }
        }
    }
    public void EquipClothe(EquippableItem newClothe)
    {
        foreach (var item in equippmentSlots)
        {
            if (item.ReceivedType == newClothe.Type)
            {
                if (item.CurrentItems == null)
                {
                    item.AddItem(newClothe);
                    break;
                }

                AddItem2Inventory(item.CurrentItems);
                item.DeleteCurrentItem();
                item.AddItem(newClothe);
                break;
            }
        }
    }

    public void RemoveItem(InventoryItemBehaviour item)
    {
        if (item is EquippableItem)
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.CurrentItems == item)
                {
                    Destroy(item.gameObject);
                    break;
                }
            }

            return;
        }

        foreach (var slot in inventorySlots)
        {
            if (slot.CurrentItems == item)
            {

                break;
            }
        }
    }
}
