using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClotheSlots : InventorySlot
{
    [SerializeField] EquippmentType receivedType;

    public EquippmentType ReceivedType { get => receivedType;}

    public override void OnDrop(PointerEventData eventData)
    {
        InventoryItemBehaviour droppedItem = eventData.pointerDrag.GetComponent<InventoryItemBehaviour>();

        if (!(droppedItem is EquippableItem) && (droppedItem as EquippableItem).Type != receivedType)
        {
            Debug.LogError("This Slot is for Equippment of type" + receivedType.ToString());
            return;
        }

        (droppedItem as EquippableItem).Use();
    }
    public override void AddItem(InventoryItemBehaviour newItem)
    {
        if (!(newItem is EquippableItem) || (newItem as EquippableItem).Type != receivedType)
        {
            Debug.LogError("This Slot is for Equippment of type" + receivedType.ToString());
            return;
        }

        base.AddItem(newItem);
    }
}
