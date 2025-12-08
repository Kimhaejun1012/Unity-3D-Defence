using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterHealth : MonoBehaviour, IDamageable
{
    public event Action OnDie;
    public event Action<float> OnHealthChanged;

    public bool isDie = false;
    protected int maxHP;
    protected int currentHP;
    public void Init(int hp)
    {
        maxHP = hp;
        currentHP = hp;
        isDie = false;
    }
    public virtual void TakeDamage(int damage)
    {
        currentHP -= damage;
        OnHealthChanged?.Invoke((float)currentHP / maxHP);

        if (currentHP <= 0f)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDie = true;
        OnDie?.Invoke();

        OnDie = null;
    }
}
