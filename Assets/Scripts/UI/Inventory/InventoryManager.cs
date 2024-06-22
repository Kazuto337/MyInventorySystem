using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public static InventoryManager Instance { get => instance; }

    public UnityEvent<ClotheBehaviour> onClotheEquipped;
    public UnityEvent<ClotheBehaviour> onClotheRemoved;

    [SerializeField] List<ClotheSlots> equippmentSlots;
    [SerializeField] List<InventorySlot> inventorySlots;

    [SerializeField] GameObject hatHandler, shirtHandler;
    public List<InventorySlot> Slots { get => inventorySlots; }
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

                ClotheBehaviour clotheBehaviour = item.CurrentItems.GetComponent<EquippableItem>().ClotheItem.GetComponent<ClotheBehaviour>();
                UnequipClothe(clotheBehaviour);
                AddItem2Inventory(item.CurrentItems);

                item.DeleteCurrentItem();
                item.AddItem(newClothe);
                break;
            }
        }

        switch (newClothe.Type)
        {
            case EquippmentType.Hat:
                ClotheBehaviour indexHat = Instantiate(newClothe.ClotheItem, hatHandler.transform , false).GetComponent<ClotheBehaviour>();
                onClotheEquipped.Invoke(indexHat);
                break;
            case EquippmentType.Shirt:
                ClotheBehaviour indexShirt = Instantiate(newClothe.ClotheItem, shirtHandler.transform).GetComponent<ClotheBehaviour>();
                onClotheEquipped.Invoke(indexShirt);
                break;
            default:
                break;
        }
    }

    public void UnequipClothe(ClotheBehaviour item)
    {
        try
        {
            switch (item.Type)
            {
                case EquippmentType.Hat:
                    onClotheRemoved.Invoke(item);
                    Transform currentHat = hatHandler.transform.GetChild(0);
                    Destroy(currentHat.gameObject);
                    break;
                case EquippmentType.Shirt:
                    onClotheRemoved.Invoke(item);
                    Transform currentShirt = shirtHandler.transform.GetChild(0);
                    Destroy(currentShirt.gameObject);
                    break;
                default:
                    break;
            }
        }
        catch (System.Exception)
        {

            throw;
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
                (item as ConsumableItem).RemoveItem();
                break;
            }
        }
    }

    private void OnDisable()
    {
        onClotheEquipped.RemoveAllListeners();
        onClotheRemoved.RemoveAllListeners();
    }
}
