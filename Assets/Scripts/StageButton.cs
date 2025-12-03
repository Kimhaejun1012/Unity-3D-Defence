using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public int stageNumber;

    public void OnStageClicked()
    {
        StageManager.SelectStage(stageNumber);
        SceneManager.LoadScene("Game");
    }
}
