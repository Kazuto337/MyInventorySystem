using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : InventoryItemBehaviour
{
    [SerializeField]protected int value;

    public override void Use()
    {
        RemoveItem();
    }
}
