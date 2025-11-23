using System.Diagnostics;

public class SlowEffect : StatusEffect
{
    public float SlowPercent => slowPercent;
    public float Duration => duration;

    private float slowPercent;
    private bool applied = false;

    private float originalSpeed;

    public SlowEffect(Monster monster, float slowPercent, float duration)
        : base(monster, duration)
    {
        this.slowPercent = slowPercent;
    }

    public override void Apply()
    {
        if (applied) return;

        originalSpeed = monster.Movement.Speed;

        monster.Movement.Speed = originalSpeed * (1f - slowPercent);

        applied = true;
    }

    public override void UpdateEffect(float dt)
    {
        duration -= dt;
    }

    public override void Remove()
    {
        monster.Movement.Speed = originalSpeed;
    }
    public void Refresh(float newDuration)
    {
        duration = newDuration;
    }
}
