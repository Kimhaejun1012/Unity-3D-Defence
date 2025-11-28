using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPrjectile : ProjectileBase
{
    public string key;
    protected override void OnHit(Monster monster)
    {
        monster.GetComponent<MonsterHealth>().TakeDamage(damage);
        ObjectPoolManager.Instance.Spawn(key, transform.position, Quaternion.identity);
    }
}
