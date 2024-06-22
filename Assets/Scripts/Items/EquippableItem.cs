using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquippmentType
{
    Hat,
    Shirt
}
public class EquippableItem : InventoryItemBehaviour
{
    [SerializeField] EquippmentType type;
    [SerializeField] int value;
    [SerializeField] GameObject clotheItem;

    public EquippmentType Type { get => type; }
    public int Value { get => value; }
    public GameObject ClotheItem { get => clotheItem; }

    public override void OnItemDropped(Transform newParent)
    {
        if (parentBeforeDrag == null)
        {
            parentBeforeDrag = newParent;
            transform.SetParent(parentBeforeDrag, false);

            return;
        }

        if (parentBeforeDrag.TryGetComponent(out ClotheSlots indexClotheSlot))
        {
            ClotheBehaviour myClothe = clotheItem.GetComponent<ClotheBehaviour>();
            InventoryManager.Instance.UnequipClothe(myClothe);
        }

        if (newParent != parentBeforeDrag)
        {
            parentBeforeDrag.GetComponent<InventorySlot>().DeleteCurrentItem();
        }
        parentBeforeDrag = newParent;
        transform.SetParent(parentBeforeDrag, false);
    }
    public override void Use()
    {
        InventoryManager.Instance.EquipClothe(this);

        InventorySlot parentSlot;
        if (transform.root.TryGetComponent(out parentSlot))
        {
            parentSlot.DeleteCurrentItem();
        }
    }
}
