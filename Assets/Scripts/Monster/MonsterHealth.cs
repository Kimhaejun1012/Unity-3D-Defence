using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MonsterHealth : MonoBehaviour, IDamageable
{
    public event Action OnDie;
    public bool isDie = false;
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
        isDie = true;
        OnDie?.Invoke();
    }
}
