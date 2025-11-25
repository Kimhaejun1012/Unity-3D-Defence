using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Tower Panel")]
    public GameObject towerInfoPanel;
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI towerDescriptionText;

    public TextMeshProUGUI goldText;
    public TextMeshProUGUI hpText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Localization.LoadLanguage("en");
    }
    private void Start()
    {
        UpdateGoldUI(PlayerStatsManager.Instance.gold);
        UpdateHPUI(PlayerStatsManager.Instance.currentHP);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(Localization.Get("TOWER_STANDARD_NAME"));
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(Localization.Get("TOWER_ICE_NAME"));
        }
    }

    public void ShowTowerInfo(string towerKey)
    {
        towerInfoPanel.SetActive(true);

        towerNameText.text = Localization.Get($"TOWER_{towerKey}_NAME");
        towerDescriptionText.text = Localization.Get("TOWER_" + towerKey + "_DESC");
    }

    public void HideTowerInfo()
    {
        towerInfoPanel.SetActive(false);
    }

    public void UpdateGoldUI(int gold)
    {
        goldText.text = $"Gold: {PlayerStatsManager.Instance.gold}";
    }

    public void UpdateHPUI(int hp)
    {
        hpText.text = $"Life: {PlayerStatsManager.Instance.currentHP}";
    }

}