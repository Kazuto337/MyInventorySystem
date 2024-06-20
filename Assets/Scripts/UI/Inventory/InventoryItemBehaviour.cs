using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class InventoryItemBehaviour : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField] int itemsAmount;
    [SerializeField] TMP_Text countTextBox;
    Image icon;
    bool itemMaxed;

    [SerializeField] InventoryItem itemData;
    Transform parentBeforeDrag;

    public bool ItemMaxed { get => itemMaxed;}
    public InventoryItem ItemData { get => itemData;}
    public int ItemsAmount { get => itemsAmount;}

    private void Awake()
    {
        icon = GetComponent<Image>();
        icon.sprite = itemData.IconSprite;
    }

    private void Start()
    {
        itemsAmount = 1;
        RefreshAmount();
    }

    public void AddItem(int newAmount)
    {
        itemsAmount+= newAmount;
        itemMaxed = itemsAmount == 5;
        RefreshAmount();    
    }
    public void RemoveItem()
    {
        itemsAmount--;

        if (itemsAmount <= 0 )
        {
            Destroy(gameObject);
        }
        RefreshAmount();
    }

    public void RefreshAmount()
    {
        countTextBox.text = itemsAmount.ToString();
        bool textBoxActiveState = itemsAmount > 1;
        countTextBox.gameObject.SetActive(textBoxActiveState);
    }

    #region Drag&Drop
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.LogWarning("On Beggin Drag " + gameObject.name);

        parentBeforeDrag = transform.parent;
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

        transform.SetParent(parentBeforeDrag, false);

        icon.raycastTarget = true;
    } 
    public void OnItemDropped(Transform newParent)
    {
        if (newParent != parentBeforeDrag)
        {
            parentBeforeDrag.GetComponent<InventorySlots>().RemoveCurrentItem();
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
