using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOnShop : MonoBehaviour
{
    [SerializeField] List<ShopItem> inventorySlots;

    private void OnEnable()
    {
        PlayerBehaviour player = GameManager.Instance.Player;

        for (int i = 0; i < player.Inventory.Slots.Count; i++)
        {
            if (player.Inventory.Slots[i].CurrentItems != null)
            {
                Debug.Log("a");
            }
            inventorySlots[i].SetItem(player.Inventory.Slots[i].CurrentItems);
            inventorySlots[i].isBuyable = false;
        }
    }
}
