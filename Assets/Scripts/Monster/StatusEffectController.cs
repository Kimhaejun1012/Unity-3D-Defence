using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectController : MonoBehaviour
{
    private List<StatusEffect> effects = new List<StatusEffect>();

    public void AddEffect(StatusEffect effect)
    {
        if (effect is SlowEffect newSlow)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                if (effects[i] is SlowEffect oldSlow)
                {
                    if (oldSlow.SlowPercent > newSlow.SlowPercent)
                        return;

                    if (Mathf.Approximately(oldSlow.SlowPercent, newSlow.SlowPercent))
                    {
                        oldSlow.Refresh(newSlow.Duration);
                        return;
                    }

                    oldSlow.Remove();
                    effects.RemoveAt(i);
                    break;
                }
            }
        }

        effects.Add(effect);
        effect.Apply();
    }


    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = effects.Count - 1; i >= 0; i--)
        {
            effects[i].UpdateEffect(dt);

            if (effects[i].IsDone)
            {
                effects[i].Remove();
                effects.RemoveAt(i);
            }
        }
    }
}
