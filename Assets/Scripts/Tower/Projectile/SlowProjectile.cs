using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowProjectile : ProjectileBase
{
    public float slowPercent;
    public float slowDuration;

    protected override void OnHit(Monster monster)
    {
        monster.GetComponent<MonsterHealth>().TakeDamage(damage);
        monster.ApplySlow(slowPercent, slowDuration);
    }
}
