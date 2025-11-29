using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAreaAttackModule : MonoBehaviour, IAttackModule
{
    [Header("Stun Settings")]
    public float stunDuration = 0.3f;

    public string attackEffectKey;
    private TowerData data;

    private void Awake()
    {
        data = GetComponent<TowerBase>().data;
    }

    public void Execute(Monster target)
    {
        ObjectPoolManager.Instance.Spawn(
            attackEffectKey,
            transform.position,
            Quaternion.identity
        );

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            data.range,
            LayerMask.GetMask("Monster")
        );

        foreach (var col in hits)
        {
            Monster m = col.GetComponent<Monster>();
            if (m == null) continue;

            m.ApplyStun(stunDuration);
        }
    }
}