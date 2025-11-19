using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StandardAttackModule : MonoBehaviour, IAttackModule
{
    public float damage = 5f;

    public void Execute(Monster target)
    {
        target.TakeDamage(damage);
    }
}
