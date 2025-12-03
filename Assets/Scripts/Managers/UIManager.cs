using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Tower Panel")]
    public GameObject towerInfoPanel;
    public TextMeshProUGUI towerNameText;
    public TextMeshProUGUI towerDescriptionText;

    [Header("Tower Stats Panel")]
    public GameObject towerStatsPanel;
    public GameObject[] starIcons;
    public TextMeshProUGUI towerStatsTitleText;
    public TextMeshProUGUI[] statsText;

    [Header("Tower Failed Panel")]
    public GameObject defeatPanel;
    public TextMeshProUGUI waveText;

    [Header("Setting Panel")]
    public GameObject optionPanel;
    public GameObject lobbyButtons;

    [Header("Pause Panel")]
    public GameObject pausePanel;

    public TextMeshProUGUI levelUpText;
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI hpText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void GameSceneInit()
    {
        UpdateGoldUI();
        UpdateHPUI();
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            if (UIManager.Instance != null)
                UIManager.Instance.GameSceneInit();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(Localization.Get("Damage"));
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(Localization.Get("TOWER_ICE_NAME"));
        }
    }

    public void ShowTowerInfo(string towerKey)
    {
        towerInfoPanel.SetActive(true);

        towerNameText.text = Localization.Get($"{towerKey}_NAME");
        towerDescriptionText.text = Localization.Get($"{towerKey}_DESC");
    }
    public void HideTowerInfo()
    {
        towerInfoPanel.SetActive(false);
    }
    public void ShowTowerStatsInfo(string towerKey, TowerData data, int level)
    {
        towerStatsPanel.SetActive(true);
        RefreshStatsInfo(towerKey, data, level);
    }

    public void RefreshStatsInfo(string towerKey, TowerData data, int level)
    {
        for (int i = 0; i <= level; i++)
        {
            starIcons[i].SetActive(true);
        }

        if (level < data.levelUpPrice.Length)
        {
            levelUpText.text = $"{Localization.Get("LevelUp")} : {data.levelUpPrice[level]}";
        }
        else if (level == data.maxLevel - 1)
        {
            levelUpText.text = Localization.Get("MaxLevel");
        }

        towerStatsTitleText.text = $"{Localization.Get($"{towerKey}_NAME")}";
        statsText[0].text = $"{Localization.Get("Damage")} : {data.baseDamage[level]}";
        statsText[1].text = $"{Localization.Get("Range")} : {data.range[level]}";
        statsText[2].text = $"{Localization.Get("AttackSpeed")} : {data.attackSpeed[level]}";

        string abilityKey = $"{towerKey}_{level + 1}";
        string ability = Localization.Get(abilityKey);

        statsText[3].text = ability ?? "";
    }
    public void HideTowerStatsInfo()
    {
        towerStatsPanel.SetActive(false);

        for (int i = 0; i < starIcons.Length; i++)
        {
            starIcons[i].SetActive(false);
        }
    }
    public void ShowGameOver()
    {
        defeatPanel.SetActive(true);
    }
    public void HideGameOver()
    {
        GameManager.Instance.PauseGame();
        defeatPanel.SetActive(false);
    }
    public void ShowPausePanel()
    {
        pausePanel.SetActive(true);
    }
    public void ShowOptionPanel()
    {
        lobbyButtons.SetActive(false);
        optionPanel.SetActive(true);
    }
    public void HideOptionPanel()
    {
        lobbyButtons.SetActive(true);
        optionPanel.SetActive(false);
        SaveManager.Save();
    }
    public void HidePausePanel()
    {
        pausePanel.SetActive(false);
    }
    public void UpdateGoldUI()
    {
        goldText.text = $"{Localization.Get("Gold")}: {PlayerStatsManager.Instance.gold}";
    }
    public void PauseGame()
    {
        GameManager.Instance.PauseGame();
        ShowPausePanel();
    }
    public void SetGameSpeed(float speed)
    {
        GameManager.Instance.SetGameSpeed(speed);
        HidePausePanel();
    }
    public void ResumeGame()
    {
        GameManager.Instance.ResumeGame();
        HidePausePanel();
    }
    public void StartGame()
    {
        GameManager.Instance.LoadGame();
    }
    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
    public void ExitToLobby()
    {
        GameManager.Instance.ResumeGame();
        SceneManager.LoadScene("Lobby");
    }
    public void ReTryGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void UpdateHPUI()
    {
        hpText.text = $"{Localization.Get("Life")}: {PlayerStatsManager.Instance.currentHP}";
    }
    public void OnClickLevelUp()
    {
        TowerManager.Instance.CurrentTower.LevelUp();
        TowerBase currentTower = TowerManager.Instance.CurrentTower;

        RefreshStatsInfo(currentTower.data.key, currentTower.data, currentTower.level);
    }
}