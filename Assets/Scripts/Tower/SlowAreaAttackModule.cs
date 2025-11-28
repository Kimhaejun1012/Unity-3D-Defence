using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAreaAttackModule : MonoBehaviour, IAttackModule
{
    public float radius;
    public float duration;
    public float slowPercent;

    public string attackEffectKey;
    public void Execute(Monster ignored)
    {
        ObjectPoolManager.Instance.Spawn(
            attackEffectKey,
            transform.position,
            Quaternion.identity
        );
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Monster"));

        foreach (var hit in hits)
        {
            Monster m = hit.GetComponent<Monster>();
            if (m == null) continue;
            m.ApplySlow(slowPercent, duration);
        }
    }
}
