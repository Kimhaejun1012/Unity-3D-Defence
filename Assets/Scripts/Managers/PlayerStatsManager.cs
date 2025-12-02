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
        UIManager.Instance.UpdateGoldUI();
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UIManager.Instance.UpdateGoldUI();
            return true;
        }
        return false;
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        UIManager.Instance.UpdateHPUI();

        if (currentHP <= 0)
        {
            UIManager.Instance.ShowGameOver();
            GameManager.Instance.GameOver();
        }
    }
    public bool CheckGold(int amount)
    {
        return gold >= amount;
    }
}
