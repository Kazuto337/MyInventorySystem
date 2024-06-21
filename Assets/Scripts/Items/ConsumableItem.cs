using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConsumableItem : InventoryItemBehaviour
{
    [SerializeField]protected int value;

    [SerializeField] int itemsAmount;
    [SerializeField] TMP_Text countTextBox;
    bool itemMaxed;

    public bool ItemMaxed { get => itemMaxed; }
    public int ItemsAmount { get => itemsAmount; }

    private void Awake()
    {
        RefreshAmount();
    }
    public void AddItem(int newAmount)
    {
        itemsAmount += newAmount;
        itemMaxed = itemsAmount == 5;
        RefreshAmount();
    }
    public void RemoveItem()
    {
        itemsAmount--;

        if (itemsAmount <= 0)
        {
            Destroy(gameObject);
        }
        RefreshAmount();
    }
    public void RefreshAmount()
    {
        itemMaxed = itemsAmount == 5;

        countTextBox.text = itemsAmount.ToString();
        bool textBoxActiveState = itemsAmount > 1;
        countTextBox.gameObject.SetActive(textBoxActiveState);
    }

    public override void Use()
    {
        RemoveItem();
    }
}
