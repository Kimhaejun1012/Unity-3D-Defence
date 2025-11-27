using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowProjectile : ProjectileBase
{
    public int damage;
    public float slowPercent;
    public float slowDuration;

    protected override void OnHit(Monster monster, Vector3 pos)
    {
        monster.GetComponent<MonsterHealth>().TakeDamage(damage);
        monster.ApplySlow(slowPercent, slowDuration);
    }
}
