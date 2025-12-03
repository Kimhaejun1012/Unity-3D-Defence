using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;
    public Transform targetPoint;

    private MonsterMovement movement;
    private MonsterHealth health;
    private MonsterUI monsterUI;

    private Collider col;
    void Awake()
    {
        movement = GetComponent<MonsterMovement>();
        health = GetComponent<MonsterHealth>();
        monsterUI = GetComponentInChildren<MonsterUI>();
        col = GetComponent<Collider>();
    }
    private void OnEnable()
    {
        health.OnDie += MonsterDie;
    }

    private void OnDisable()
    {
        health.OnDie -= MonsterDie;
    }
    public void Init(Transform[] transforms)
    {
        health.Init(data.maxHP);
        movement.Init(data.moveSpeed, transforms);
        monsterUI.Init();
        col.enabled = true;
    }
    private void MonsterDie()
    {
        PlayerStatsManager.Instance.AddGold(data.reward);
        col.enabled = false;
    }

    public void ApplySlow(float percent, float duration)
    {
        movement.ApplySlow(percent, duration);
    }
    public void ApplyStun(float t)
    {
        movement.ApplyStun(t);
    }
}
