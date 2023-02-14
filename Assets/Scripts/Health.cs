using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public event Action<float, float> Changed;

    protected float CurrentHealth;
    
    public float MaxHealth => _maxHealth;

    private void Start()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        Changed?.Invoke(CurrentHealth, MaxHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Reset()
    {
        CurrentHealth = _maxHealth;
        Changed?.Invoke(CurrentHealth, MaxHealth);
    }

    public void IncreaseMaxHealth(int value)
    {
        if (value > 0)
            _maxHealth += value;

        Reset();
    }

    protected abstract void Die();
}