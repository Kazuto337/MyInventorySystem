using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    private bool isInventoryOpen , isShopOpen;

    [SerializeField] GameObject inventoryView;
    [SerializeField] GameObject shopView;

    private void OnEnable()
    {
        GameManager.Instance.Player.OnInventoryOpened.AddListener(ToggleInventory);
    }
    private void Start()
    {
        isInventoryOpen = false;
        isShopOpen = false;

        inventoryView.SetActive(isInventoryOpen);
        inventoryView.SetActive(isShopOpen);
    }
    
    public void ToggleInventory()
    {
        if (isShopOpen)
        {
            isShopOpen = false;
            shopView.SetActive(false);

            isInventoryOpen = true;
            inventoryView.SetActive(true);
            return;
        }

        isInventoryOpen = !isInventoryOpen;
        inventoryView.SetActive(isInventoryOpen);
    }

    public void ToggleShopView()
    {
        isShopOpen = !isShopOpen;
        shopView.SetActive(isShopOpen);
    }
}
