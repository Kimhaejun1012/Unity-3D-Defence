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
}
