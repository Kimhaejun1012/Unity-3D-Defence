using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackModule : MonoBehaviour, IAttackModule
{
    public string projectileKey;
    public Transform firePoint;

    public float projectileSpeed = 10f;

    public void Execute(Monster target)
    {
        if (target == null) return;

        GameObject proj = ObjectPoolManager.Instance.Spawn(
            projectileKey,
            firePoint.position,
            firePoint.rotation
        );

        Projectile p = proj.GetComponent<Projectile>();
        p.Init(target, projectileSpeed);
    }
}
