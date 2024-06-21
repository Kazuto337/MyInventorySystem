using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItemBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    Image icon;

    [SerializeField] protected MenuItem itemData;
    [SerializeField] bool canDrag;
    protected Transform parentBeforeDrag;

    public MenuItem ItemData { get => itemData; }

    private void Awake()
    {
        icon = GetComponent<Image>();
        icon.sprite = itemData.IconSprite;

        if (transform.parent.TryGetComponent<InventorySlot>(out InventorySlot slot))
        {
            parentBeforeDrag = transform.parent;
        }
    }

    #region Drag&Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }
        Debug.LogWarning("On Beggin Drag " + gameObject.name);

        parentBeforeDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }
        Debug.LogWarning("On Drag " + gameObject.name);
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }

        Debug.LogWarning("On End Drag " + gameObject.name);

        transform.SetParent(parentBeforeDrag, false);

        icon.raycastTarget = true;
    }
    public virtual void OnItemDropped(Transform newParent)
    {
        if (newParent != parentBeforeDrag)
        {
            parentBeforeDrag.GetComponent<InventorySlot>().DeleteCurrentItem();
        }
        parentBeforeDrag = newParent;
        transform.SetParent(parentBeforeDrag, false);
    }
    #endregion

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogWarning(gameObject.name + "Used");
        Use();
    }

    public virtual void Use()
    {
        Debug.Log("Father");
    }
}
