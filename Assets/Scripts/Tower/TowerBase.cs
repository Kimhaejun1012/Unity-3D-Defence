using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour
{
    public GameObject[] auraObjects;

    public TowerData data;
    public int level = 0;

    public event Action OnFire;
    public GameObject rangeVisualizer;
    
    private float attackTimer = 0f;
    private IAttackModule[] attackModules;
    Monster target;

    private void Awake()
    {
        attackModules = GetComponents<IAttackModule>();

        rangeVisualizer.SetActive(false);
        float scale = data.range[level] * 2f;
        rangeVisualizer.transform.localScale = new Vector3(scale, 0.01f, scale);
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;

        target = TargetFinder.GetNearestEnemy(transform.position, data.range[level]);

        transform.LookAt(target?.transform);
        Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, rot.y, 0);

        if (attackTimer >= 1f / data.attackSpeed[level] && target != null)
        {
            AnimationTrigger();
            attackTimer = 0f;
        }
    }
    public void AnimationTrigger()
    {
        OnFire?.Invoke();
    }
    public void ModulesExecute()
    {
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
    public void LevelUp()
    {
        if(level < data.maxLevel - 1 && PlayerStatsManager.Instance.CheckGold(data.levelUpPrice[level]))
        {
            PlayerStatsManager.Instance.SpendGold(data.levelUpPrice[level]);
            UpdateAura();
            level++;
            float scale = data.range[level] * 2f;
            rangeVisualizer.transform.localScale = new Vector3(scale, 0.01f, scale);
        }
    }
    private void UpdateAura()
    {
        if (level < auraObjects.Length && auraObjects[level] != null)
            auraObjects[level].SetActive(true);
    }

}
