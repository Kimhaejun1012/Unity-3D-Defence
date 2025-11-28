using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StandardAttackModule : MonoBehaviour, IAttackModule
{
    public int damage = 5;

    public void Execute(Monster target)
    {
        target.GetComponent<IDamageable>().TakeDamage(damage);
    }
}
