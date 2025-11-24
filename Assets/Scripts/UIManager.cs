using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject towerInfoPanel;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        towerInfoPanel.SetActive(false);

        Localization.LoadLanguage("en");
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
    }

    public void HideTowerInfo()
    {
        towerInfoPanel.SetActive(false);
    }
}