using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour, IDamageable
{
    public event Action OnDie;
    private int currentHP;
    public void Init(int hp)
    {
        currentHP = hp;
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDie?.Invoke();
        GetComponent<PooledObject>().Return();
    }
}
