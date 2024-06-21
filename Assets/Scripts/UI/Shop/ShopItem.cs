using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    ShopManager shopManager;
    [SerializeField] InventoryItemBehaviour item;
    public Transform initialSlot;
    public bool isBuyable;

    [SerializeField] Image icon;
    [SerializeField] TMP_Text text;

    public InventoryItemBehaviour Item { get => item;}

    private void Start()
    {
        shopManager = ShopManager.Instance;

        if (item != null)
        {
            icon.sprite = item.ItemData.IconSprite;
        }

        if (item is ConsumableItem)
        {
            RefreshAmount();
        }
        else
        {
            text.gameObject.SetActive(false);
        }

    }

    public void RefreshAmount()
    {
        if ((item is ConsumableItem) == false)
        {
            return;
        }

        text.text = (item as ConsumableItem).ItemsAmount.ToString();
        bool textBoxActiveState = (item as ConsumableItem).ItemsAmount > 1;
        text.gameObject.SetActive(textBoxActiveState);
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

        if (gm.Player.Coins < item.ItemData.Price)
        {
            //not enough money
            Debug.Log("Not Enough Money");
            return;
        }

        InventoryItemBehaviour newItem = Instantiate(item.gameObject , initialSlot).GetComponent<InventoryItemBehaviour>();

        gm.Player.OnCoinsRemoved.Invoke(newItem.ItemData.Price);
        gm.Player.Inventory.AddItem2Inventory(newItem);
        shopManager.RefreshShopInventory();

        RefreshAmount();

    }

    private void Sell()
    {
        GameManager gm = GameManager.Instance;
        gm.Player.OnCoinsAdded.Invoke(item.ItemData.Price);
        gm.Player.Inventory.RemoveItem(item);

        icon.enabled = false;
        item = null;
        shopManager.RefreshShopInventory();

        RefreshAmount();
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

        RefreshAmount();
    }
}
