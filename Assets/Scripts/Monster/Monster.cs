using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;

    private MonsterMovement movement;
    private MonsterHealth health;

    void Awake()
    {
        movement = GetComponent<MonsterMovement>();
        health = GetComponent<MonsterHealth>();
    }

    public void Init(Transform[] transforms)
    {
        health.Init(data.maxHP);
        movement.Init(data.moveSpeed, transforms);
    }
}
