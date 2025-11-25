using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    public int gold;
    public int maxHP;
    public int currentHP;
    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void AddGold(int amount)
    {
        gold += amount;
        UIManager.Instance.UpdateGoldUI(gold);
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UIManager.Instance.UpdateGoldUI(gold);
            return true;
        }
        return false;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        UIManager.Instance.UpdateHPUI(currentHP);

        if (currentHP <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    public bool CheckGold(int amount)
    {
        return gold >= amount;
    }
}
