using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] UnityEvent<float> onDamageReceived, onHealingReceived;
    [SerializeField] UnityEvent<int> onCoinsAdded, onCoinsRemoved;
    [SerializeField] UnityEvent onInventoryOpened;

    [SerializeField] float healthPoints;
    [SerializeField] int coins;

    [SerializeField] InventoryManager inventoryManager;

    [SerializeField] PlayerMovement playerMovement;

    #region Getters
    public float HealthPoints { get => healthPoints; }
    public int Coins { get => coins; }

    public UnityEvent<float> OnDamageReceived { get => onDamageReceived; }
    public UnityEvent<float> OnHealingReceived { get => onHealingReceived; }
    public UnityEvent<int> OnCoinsAdded { get => onCoinsAdded; }
    public UnityEvent<int> OnCoinsRemoved { get => onCoinsRemoved; }

    public UnityEvent OnInventoryOpened { get => onInventoryOpened; }
    public InventoryManager Inventory { get => inventoryManager; } 
    #endregion

    private void OnEnable()
    {
        onHealingReceived.AddListener(Heal);
        onDamageReceived.AddListener(ReceiveDamage);

        onCoinsAdded.AddListener(AddCoins);
        onCoinsRemoved.AddListener(RemoveCoins);

        inventoryManager.onClotheEquipped.AddListener(ChangeClothes);
        inventoryManager.onClotheRemoved.AddListener(RemoveClothe);
    }

    #region Modifiers

    public void ChangeClothes(ClotheBehaviour newClothe)
    {
        int currentClothePosition = playerMovement.clothes.Count;
        for (int i = 0; i < playerMovement.clothes.Count; i++)
        {
            if (playerMovement.clothes[i].Type == newClothe.Type)
            {
                currentClothePosition = i;
                break;
            }
        }

        if (currentClothePosition == playerMovement.clothes.Count)
        {
            playerMovement.clothes.Add(newClothe);
            return;
        }

        playerMovement.clothes[currentClothePosition] = newClothe;
    }
    public void RemoveClothe(ClotheBehaviour clothe)
    {
        for (int i = 0; i < playerMovement.clothes.Count; i++)
        {
            if (clothe.Type == playerMovement.clothes[i].Type)
            {
                playerMovement.clothes.RemoveAt(i);
                break;
            }
        }
    }
    public void OpenInventory()
    {
        OnInventoryOpened.Invoke();
    }

    private void Heal(float extraHp)
    {
        healthPoints += extraHp;
    }
    private void ReceiveDamage(float damage)
    {
        healthPoints -= damage;
    }

    private void AddCoins(int coinIn)
    {
        coins += coinIn;
    }

    private void RemoveCoins(int coinOut)
    {
        coins -= coinOut;
    }

    #endregion
    private void OnDisable()
    {
        onHealingReceived.RemoveAllListeners();
        onDamageReceived.RemoveAllListeners();

        onCoinsAdded.RemoveAllListeners();
        onCoinsRemoved.RemoveAllListeners();
    }
}
