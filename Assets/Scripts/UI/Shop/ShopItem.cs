using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] InventoryItemBehaviour item;
    public bool isBuyable;

    [SerializeField] Image icon;

    private void Start()
    {
        if (item != null)
        {
            icon.sprite = item.ItemData.IconSprite;
        }
    }

    public void BuyOrSell()
    {
        if (item == null)
        {
            return;
        }
        if (isBuyable)
        {
            Debug.Log("Buying Object");
            Buy();
            return;
        }

        Debug.Log("Selling Object");
        Sell();
    }

    private void Buy()
    {
        GameManager gm = GameManager.Instance;
        gm.Player.OnCoinsRemoved.Invoke(item.ItemData.Price);
        gm.Player.Inventory.AddItem2Inventory(item);

    }

    private void Sell()
    {
        GameManager gm = GameManager.Instance;
        gm.Player.OnCoinsAdded.Invoke(item.ItemData.Price);
        gm.Player.Inventory.RemoveItem(item);

        icon.enabled = false;
        item = null;
    }

    public void SetItem(InventoryItemBehaviour newItem)
    {
        item = newItem;

        if (item == null)
        {
            icon.enabled = false;
            item = null;

            return;
        }

        icon.sprite = newItem.ItemData.IconSprite;
        icon.enabled = true;
    }
}
