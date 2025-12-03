using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StageManager
{
    public static int selectedStage = 1;

    public static void SelectStage(int stage)
    {
        selectedStage = stage;
        Debug.Log("스테이지: " + stage);
    }
}
