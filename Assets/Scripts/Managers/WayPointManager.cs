using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    public static WayPointManager Instance;

    public Transform[] waypointParent;

    [HideInInspector]
    public Transform[] waypoints;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        LoadWaypoints();
    }

    private void LoadWaypoints()
    {
        if (waypointParent == null)
        {
            return;
        }

        int count = waypointParent[StageManager.selectedStage].childCount;
        waypoints = new Transform[count];

        for (int i = 0; i < count; i++)
        {
            waypoints[i] = waypointParent[StageManager.selectedStage].GetChild(i);
        }
    }
}
