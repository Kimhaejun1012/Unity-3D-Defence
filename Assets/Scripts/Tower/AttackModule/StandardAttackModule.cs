using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class StandardAttackModule : MonoBehaviour, IAttackModule
{
    public Transform firePoint;
    public string muzzleKey;

    private TowerBase tower;

    private void Awake()
    {
        tower = GetComponent<TowerBase>();
    }

    public void Execute(Monster target)
    {
        ObjectPoolManager.Instance.Spawn(
            muzzleKey,
            target.targetPoint.position,
            target.targetPoint.rotation
        );

        var damageable = target.GetComponent<IDamageable>();
        damageable?.TakeDamage(tower.data.baseDamage[tower.level]);

        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxDB.bladeHit);
    }
}
