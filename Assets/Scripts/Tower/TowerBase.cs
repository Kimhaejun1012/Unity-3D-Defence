using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    [Header("Tower Stats")]
    public float range = 5f;
    public float attackSpeed = 1f;

    public GameObject rangeVisualizer;

    private float attackTimer = 0f;
    private IAttackModule[] attackModules;

    private void Awake()
    {
        attackModules = GetComponents<IAttackModule>();

        rangeVisualizer.SetActive(false);
        float scale = range * 2f;
        rangeVisualizer.transform.localScale = new Vector3(scale, 0.01f, scale);
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
    public void ShowRange(bool show)
    {
        rangeVisualizer.SetActive(show);
    }
}
