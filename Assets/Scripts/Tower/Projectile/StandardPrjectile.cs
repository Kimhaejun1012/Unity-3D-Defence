using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPrjectile : ProjectileBase
{
    public int damage;

    protected override void OnHit(Monster monster)
    {
        monster.GetComponent<MonsterHealth>().TakeDamage(damage);
    }
}
