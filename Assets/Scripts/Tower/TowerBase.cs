using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Tower Stats")]
    public float range = 5f;
    public float attackSpeed = 1f;

    private float attackTimer = 0f;
    private IAttackModule[] attackModules;

    private void Awake()
    {
        attackModules = GetComponents<IAttackModule>();
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= 1f / attackSpeed)
        {
            attackTimer = 0f;
            Attack();
        }
    }

    private void Attack()
    {
        Monster target = TargetFinder.GetNearestEnemy(transform.position, range);
        if (target == null) return;

        foreach (var module in attackModules)
        {
            module.Execute(target);
        }
    }
}
