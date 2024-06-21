using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    static ShopManager instance;

    [SerializeField] List<ShopItem> inventorySlots;

    public static ShopManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else instance = this;
    }

    private void OnEnable()
    {
        RefreshShopInventory();        
    }

    [ContextMenu("RefreshShopInventory")]
    public void RefreshShopInventory()
    {
        List<InventorySlot> currentInventory = GameManager.Instance.Player.Inventory.Slots;

        for (int i = 0; i < currentInventory.Count; i++)
        {
            inventorySlots[i].SetItem(currentInventory[i].CurrentItems);
            inventorySlots[i].isBuyable = false;
        }
    }
}
