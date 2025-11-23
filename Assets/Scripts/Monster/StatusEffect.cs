using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusEffect
{
    public float duration;
    protected Monster monster;
    public bool IsDone => duration <= 0f;

    public StatusEffect(Monster monster, float duration)
    {
        this.monster = monster;
        this.duration = duration;
    }

    public abstract void Apply();
    public abstract void UpdateEffect(float dt);
    public abstract void Remove();
}
