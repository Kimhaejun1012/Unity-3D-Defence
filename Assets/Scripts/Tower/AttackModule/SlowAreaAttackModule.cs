using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAreaAttackModule : MonoBehaviour, IAttackModule
{
    public float duration;
    public float slowPercent;
    private TowerData data;
    public string attackEffectKey;

    private void Awake()
    {
        data = GetComponent<TowerBase>().data;
    }

    public void Execute(Monster ignored)
    {
        ObjectPoolManager.Instance.Spawn(
            attackEffectKey,
            transform.position,
            Quaternion.identity
        );
        Collider[] hits = Physics.OverlapSphere(transform.position, data.range, LayerMask.GetMask("Monster"));

        foreach (var hit in hits)
        {
            Monster m = hit.GetComponent<Monster>();
            if (m == null) continue;
            m.ApplySlow(slowPercent, duration);
        }
    }
}
