using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardPrjectile : ProjectileBase
{
    public int damage;
    public string key;
    protected override void OnHit(Monster monster, Vector3 pos)
    {
        monster.GetComponent<MonsterHealth>().TakeDamage(damage);
        ObjectPoolManager.Instance.Spawn(key, pos, Quaternion.identity);
    }
}
