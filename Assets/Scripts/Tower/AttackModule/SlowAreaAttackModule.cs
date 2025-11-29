using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAreaAttackModule : MonoBehaviour, IAttackModule
{
    public float[] duration = { 0.1f, 0.2f, 0.3f };
    public float[] slowPercent = { 0.1f, 0.2f, 0.3f };
    public string attackEffectKey;
    private TowerBase tower;

    private void Awake()
    {
        tower = GetComponent<TowerBase>();
    }

    public void Execute(Monster ignored)
    {
        ObjectPoolManager.Instance.Spawn(
            attackEffectKey,
            transform.position,
            Quaternion.identity
        );
        Collider[] hits = Physics.OverlapSphere(transform.position, tower.data.range[tower.level], LayerMask.GetMask("Monster"));

        foreach (var hit in hits)
        {
            Monster m = hit.GetComponent<Monster>();
            if (m == null) continue;
            m.ApplySlow(slowPercent[tower.level], duration[tower.level]);
        }
    }
}
