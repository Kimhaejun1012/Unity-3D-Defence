using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningProjectile : ProjectileBase
{
    public string key;
    private int[] chainCount = { 3, 4, 5 };
    private float[] chainRange = { 4f, 5f, 6f };

    private List<Monster> hitTargets = new List<Monster>();

    public override void Init(Monster target, float speed, int damage, int level)
    {
        base.Init(target, speed, damage, level);
        hitTargets.Clear();
    }

    protected override void OnHit(Monster firstTarget)
    {
        ChainDamage(firstTarget);
    }

    private void ChainDamage(Monster startTarget)
    {
        Monster current = startTarget;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxDB.lightningHit);


        for (int i = 0; i < chainCount[level]; i++)
        {
            if (current == null) break;

            if (!hitTargets.Contains(current))
            {
                hitTargets.Add(current);
                current.GetComponent<MonsterHealth>().TakeDamage(damage);
                ObjectPoolManager.Instance.Spawn(key, current.targetPoint.position, Quaternion.identity);
            }
            current = FindNextTarget(current);
        }
    }
    private Monster FindNextTarget(Monster from)
    {
        Collider[] hits = Physics.OverlapSphere(from.transform.position, chainRange[level], LayerMask.GetMask("Monster"));

        Monster nearest = null;
        float nearestDist = float.MaxValue;

        foreach (var h in hits)
        {
            Monster m = h.GetComponent<Monster>();
            if (m == null) continue;
            if (hitTargets.Contains(m)) continue;
            if (!m.gameObject.activeInHierarchy) continue;

            float dist = Vector3.Distance(from.transform.position, m.transform.position);

            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = m;
            }
        }

        return nearest;
    }
}
