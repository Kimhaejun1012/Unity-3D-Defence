using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunAreaAttackModule : MonoBehaviour, IAttackModule
{
    private float[] stunDuration = {0.1f,0.2f,0.3f};

    public string attackEffectKey;
    private TowerBase tower;

    private void Awake()
    {
        tower = GetComponent<TowerBase>();
    }

    public void Execute(Monster target)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxDB.stunSFX);

        ObjectPoolManager.Instance.Spawn(
            attackEffectKey,
            transform.position,
            Quaternion.identity
        );

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            tower.data.range[tower.level],
            LayerMask.GetMask("Monster")
        );

        foreach (var col in hits)
        {
            Monster m = col.GetComponent<Monster>();
            if (m == null) continue;

            m.ApplyStun(stunDuration[tower.level]);
        }
    }
}