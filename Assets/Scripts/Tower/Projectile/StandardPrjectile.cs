using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPrjectile : ProjectileBase
{
    public string key;
    protected override void OnHit(Monster monster)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxDB.standardHit);

        monster.GetComponent<IDamageable>().TakeDamage(damage);
        ObjectPoolManager.Instance.Spawn(key, transform.position, Quaternion.identity);
    }
}
