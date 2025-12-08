using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonsterHealth
{
    private void Start()
    {
        SoundManager.Instance.OnBossAppeared();
        UIManager.Instance.bossHPBar.SetActive(true);
    }
    public override void TakeDamage(int damage)
    {
        currentHP -= damage;
        UIManager.Instance.UpdateBossHPUI((float)currentHP / maxHP);

        if (currentHP <= 0f)
        {
            Die();
        }
    }
    public override void Die()
    {
        base.Die();
        UIManager.Instance.bossHPBar.SetActive(false);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sfxDB.bossDie);
    }
}
