using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlots : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0)
        {
            Debug.LogWarning("Slot full: " + gameObject.name);
            return;
        }

        Debug.LogWarning("Item Succesfully Placed On " + gameObject.name);

        InvetoryItemBehaviour droppedItem = eventData.pointerDrag.GetComponent<InvetoryItemBehaviour>();
        droppedItem.OnItemDropped(gameObject.transform);
    }
}
