using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SplashProjectile : ProjectileBase
{
    public string key;

    public float splashRadius;
    public LayerMask monsterMask;

    protected override void OnHit(Monster target)
    {
        ObjectPoolManager.Instance.Spawn(key, transform.position, Quaternion.identity);
        Collider[] hits = Physics.OverlapSphere(target.transform.position, splashRadius, monsterMask);

        foreach (var h in hits)
        {
            Monster m = h.GetComponent<Monster>();
            if (m != null)
                m.GetComponent<MonsterHealth>().TakeDamage(damage);
        }
    }

}
