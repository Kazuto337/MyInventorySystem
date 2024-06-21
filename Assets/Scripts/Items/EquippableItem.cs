using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquippmentType
{
    Hat,
    Shirt,
    Pants,
    Shoes
}
public class EquippableItem : InventoryItemBehaviour
{
    [SerializeField] EquippmentType type;
    [SerializeField] int value;

    public EquippmentType Type { get => type; }
    public int Value { get => value; }

    public override void OnItemDropped(Transform newParent)
    {
        if (newParent != parentBeforeDrag)
        {
            parentBeforeDrag.GetComponent<InventorySlot>().RemoveCurrentItem();
        }
        parentBeforeDrag = newParent;
        transform.SetParent(parentBeforeDrag, false);
    }
    public override void Use()
    {
        InventoryManager.Instance.EquipClothe(this);

        InventorySlot parentSlot;
        if (transform.root.TryGetComponent<InventorySlot>(out parentSlot))
        {
            parentSlot.RemoveCurrentItem();
        }
    }
}
