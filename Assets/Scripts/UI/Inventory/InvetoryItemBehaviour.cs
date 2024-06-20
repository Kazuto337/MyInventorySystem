using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvetoryItemBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Image icon;

    [SerializeField] InventoryItem itemData;
    Transform parentAfterDrag;

    private void Awake()
    {
        icon = GetComponent<Image>();
        icon.sprite = itemData.IconSprite;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.LogWarning("On Beggin Drag " + gameObject.name);


        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.LogWarning("On Drag " + gameObject.name);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.LogWarning("On End Drag " + gameObject.name);

        transform.SetParent(parentAfterDrag, false);

        icon.raycastTarget = true;
    }

    public void OnItemDropped(Transform newParent)
    {
        parentAfterDrag = newParent;
        transform.SetParent(parentAfterDrag, false);
    }
}
