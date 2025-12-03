using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAreaAttackModule : MonoBehaviour, IAttackModule
{
    private float[] duration = { 3f, 4f, 5f };
    private float[] slowPercent = { 0.3f, 0.4f, 0.5f };
    public string attackEffectKey;
    private TowerBase tower;

    private void Awake()
    {
        tower = GetComponent<TowerBase>();
    }

    public void Execute(Monster ignored)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxDB.slowSFX);
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
            {
                m.ApplySlow(slowPercent[tower.level], duration[tower.level]);
                m.GetComponent<IDamageable>().TakeDamage(tower.data.baseDamage[tower.level]);
            }
        }
    }
}
