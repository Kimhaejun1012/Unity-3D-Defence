using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackModule : MonoBehaviour, IAttackModule
{
    public string projectileKey;
    public string muzzleKey;
    public Transform firePoint;

    public float projectileSpeed = 10f;
    private TowerBase tower;

    private void Awake()
    {
        tower = GetComponent<TowerBase>();
    }
    public void Execute(Monster target)
    {
        if (target == null) return;

        GameObject proj = ObjectPoolManager.Instance.Spawn(
            projectileKey,
            firePoint.position,
            firePoint.rotation
        );
        ObjectPoolManager.Instance.Spawn(
            muzzleKey,
            firePoint.position,
            firePoint.rotation
        );

        ProjectileBase p = proj.GetComponent<ProjectileBase>();
        p.Init(target, projectileSpeed, tower.data.baseDamage[tower.level], tower.level);
    }
}
