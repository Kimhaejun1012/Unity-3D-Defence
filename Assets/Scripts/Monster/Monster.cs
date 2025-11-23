using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData data;
    public MonsterMovement Movement { get; private set; }
    private MonsterHealth health;

    private StatusEffectController effectController;
    void Awake()
    {
        Movement = GetComponent<MonsterMovement>();
        health = GetComponent<MonsterHealth>();
        effectController = GetComponent<StatusEffectController>();
    }
    public void Init(Transform[] transforms)
    {
        health.Init(data.maxHP);
        Movement.Init(data.moveSpeed, transforms);
    }

    public void ApplySlow(float percent, float duration)
    {
        effectController.AddEffect(new SlowEffect(this, percent, duration));
    }
}
