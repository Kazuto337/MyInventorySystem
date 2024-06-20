using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] UnityEvent<float> onDamageReceived, onHealingReceived;
    [SerializeField] UnityEvent<int> onCoinsAdded, onCoinsRemoved;

    float healthPoints;
    int coins;

    public float HealthPoints { get => healthPoints; }
    public int Coins { get => coins; }

    public UnityEvent<float> OnDamageReceived { get => onDamageReceived; }
    public UnityEvent<float> OnHealingReceived { get => onHealingReceived; }
    public UnityEvent<int> OnCoinsAdded { get => onCoinsAdded; }
    public UnityEvent<int> OnCoinsRemoved { get => onCoinsRemoved; }

    private void OnEnable()
    {
        onHealingReceived.AddListener(Heal);
        onDamageReceived.AddListener(ReceiveDamage);

        onCoinsAdded.AddListener(AddCoins);
        onCoinsRemoved.AddListener(RemoveCoins);
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

    private void OnDisable()
    {
        onHealingReceived.RemoveAllListeners();
        onDamageReceived.RemoveAllListeners();

        onCoinsAdded.RemoveAllListeners();
        onCoinsRemoved.RemoveAllListeners();
    }
}
