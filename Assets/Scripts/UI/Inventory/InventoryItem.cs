using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "NewMenuDataItem", menuName = "Menu Item", order = 1)]
public class InventoryItem
    : ScriptableObject
{
    [SerializeField] Sprite iconSprite;
    [SerializeField] int price;

    public Sprite IconSprite { get => iconSprite; set => iconSprite = value; }
    public int Price { get => price; set => price = value; }
}
